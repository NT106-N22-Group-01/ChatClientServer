﻿using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;

namespace TcpServerClient
{
	public class STcpClient : IDisposable
	{
		public bool IsConnected
		{
			get
			{
				return _isConnected;
			}
			private set
			{
				_isConnected = value;
			}
		}

		public IPEndPoint LocalEndpoint
		{
			get
			{
				if (_client != null && _isConnected)
				{
					return (IPEndPoint)_client.Client.LocalEndPoint;
				}

				return null;
			}
		}

		public STcpClientSettings Settings
		{
			get
			{
				return _settings;
			}
			set
			{
				if (value == null) _settings = new STcpClientSettings();
				else _settings = value;
			}
		}

		public STcpClientEvents Events
		{
			get
			{
				return _events;
			}
			set
			{
				if (value == null) _events = new STcpClientEvents();
				else _events = value;
			}
		}

		public string ServerIpPort
		{
			get
			{
				return $"{_serverIp}:{_serverPort}";
			}
		}

		public Action<string> Logger = null;

		#region Private Member
		private readonly string _header = "[TcpClient] ";
		private STcpClientSettings _settings = new STcpClientSettings();
		private STcpClientEvents _events = new STcpClientEvents();

		private string _serverIp = null;
		private int _serverPort = 0;
		private readonly IPAddress _ipAddress = null;
		private TcpClient _client = null;
		private NetworkStream _networkStream = null;

		private readonly SemaphoreSlim _sendLock = new SemaphoreSlim(1, 1);
		private bool _isConnected = false;

		private Task _dataReceiver = null;
		private CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private CancellationToken _token;

		private bool _isTimeout = false;
		#endregion

		#region Constructors-and-Factories
		public STcpClient(string ipPort)
		{
			if (string.IsNullOrEmpty(ipPort)) throw new ArgumentNullException(nameof(ipPort));

			Util.ParseIpPort(ipPort, out _serverIp, out _serverPort);
			if (_serverPort < 0) throw new ArgumentException("Port must be zero or greater.");
			if (string.IsNullOrEmpty(_serverIp)) throw new ArgumentNullException("Server IP or hostname must not be null.");

			if (!IPAddress.TryParse(_serverIp, out _ipAddress))
			{
				_ipAddress = Dns.GetHostEntry(_serverIp).AddressList[0];
				_serverIp = _ipAddress.ToString();
			}
		}
		#endregion
		#region Public-Methods

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Connect()
		{
			// Check if the client is already connected
			if (IsConnected)
			{
				Logger?.Invoke($"{_header}already connected");
				return;
			}

			// Initialize a new TCP client and log a message
			Logger?.Invoke($"{_header}initializing client");
			_client = new TcpClient();

			// Connect to the server and log a message
			Logger?.Invoke($"{_header}connecting to {ServerIpPort}");
			var asyncResult = _client.BeginConnect(_serverIp, _serverPort, null, null);
			var waitHandle = asyncResult.AsyncWaitHandle;

			// Register a callback to close the network stream if the cancellation token is canceled
			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			_token.Register(() => _networkStream?.Close());

			// Wait for the connection to succeed or fail
			try
			{
				// Wait for a connection response or timeout
				if (!waitHandle.WaitOne(TimeSpan.FromMilliseconds(_settings.ConnectTimeoutMs), false))
				{
					// Close the client and throw a TimeoutException if the connection times out
					_client.Close();
					throw new TimeoutException($"Timeout connecting to {ServerIpPort}");
				}

				// Finish the connection and get the network stream
				_client.EndConnect(asyncResult);
				_networkStream = _client.GetStream();
			}
			catch (Exception)
			{
				throw;
			}

			// Set the connected and timeout flags, raise the Connected event, and start the DataReceiver task
			_isConnected = true;
			_isTimeout = false;
			_events.HandleConnected(this, new ConnectionEventArgs(ServerIpPort));
			_dataReceiver = Task.Run(() => DataReceiver(_token), _token);
		}

		public void Disconnect()
		{
			if (!IsConnected)
			{
				Logger?.Invoke($"{_header}already disconnected");
				return;
			}

			Logger?.Invoke($"{_header}disconnecting from {ServerIpPort}");

			_tokenSource.Cancel();
			WaitCompletion();
			_client.Close();
			_isConnected = false;
		}

		public async Task DisconnectAsync()
		{
			if (!IsConnected)
			{
				Logger?.Invoke($"{_header}already disconnected");
				return;
			}

			Logger?.Invoke($"{_header}disconnecting from {ServerIpPort}");

			_tokenSource.Cancel();
			await WaitCompletionAsync();
			_client.Close();
			_isConnected = false;
		}

		public void Send(long contentLength, Stream stream)
		{
			if (contentLength < 1) return;
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new InvalidOperationException("Cannot read from supplied stream.");
			if (!_isConnected) throw new IOException("Not connected to the server; use Connect() first.");

			SendInternal(contentLength, stream);
		}

		public async Task SendAsync(long contentLength, Stream stream, CancellationToken token = default)
		{
			if (contentLength < 1) return;
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new InvalidOperationException("Cannot read from supplied stream.");
			if (!_isConnected) throw new IOException("Not connected to the server; use Connect() first.");
			if (token == default(CancellationToken)) token = _token;

			await SendInternalAsync(contentLength, stream, token).ConfigureAwait(false);
		}
		#endregion


		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				_isConnected = false;

				if (_tokenSource != null)
				{
					if (!_tokenSource.IsCancellationRequested)
					{
						_tokenSource.Cancel();
						_tokenSource.Dispose();
					}
				}

				if (_networkStream != null)
				{
					_networkStream.Close();
					_networkStream.Dispose();
				}

				if (_client != null)
				{
					_client.Close();
					_client.Dispose();
				}

				Logger?.Invoke($"{_header}dispose complete");
			}
		}


		private async Task DataReceiver(CancellationToken token)
		{
			while (!token.IsCancellationRequested && _client != null && _client.Connected)
			{
				try
				{
					await DataReadAsync(token).ContinueWith(async task =>
					{
						if (task.IsCanceled) return default;
						var data = task.Result;

						if (data != null)
						{

							Action action = () => _events.HandleDataReceived(this, new DataReceivedEventArgs(ServerIpPort, data));
							if (_settings.UseAsyncDataReceivedEvents)
							{
								_ = Task.Run(action, token);
							}
							else
							{
								action.Invoke();
							}
							return data;
						}
						else
						{
							await Task.Delay(100).ConfigureAwait(false);
							return default;
						}

					}, token).ContinueWith(task => { }).ConfigureAwait(false);
				}
				catch (Exception ex) when (ex is IOException 
										|| ex is SocketException 
										|| ex is TaskCanceledException 
										|| ex is OperationCanceledException 
										|| ex is ObjectDisposedException)
				{
					Logger?.Invoke($"{_header}data receiver canceled, disconnected");
					break;
				}
				catch (Exception ex)
				{
					Logger?.Invoke($"{_header}data receiver exception:{Environment.NewLine}{ex}{Environment.NewLine}");
					break;
				}
			}

			Logger?.Invoke($"{_header}disconnection detected");

			_isConnected = false;

			if (!_isTimeout) _events.HandleClientDisconnected(this, new ConnectionEventArgs(ServerIpPort));
			// else _events.HandleClientDisconnected(this, new ConnectionEventArgs(ServerIpPort, DisconnectReason.Timeout)); To Do

			Dispose();
		}

		private async Task<ArraySegment<byte>> DataReadAsync(CancellationToken token)
		{
			byte[] buffer = new byte[_settings.StreamBufferSize];
			int read = 0;

			try
			{
				// Read data from the network stream asynchronously
				read = await _networkStream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);

				// If data was read successfully
				if (read > 0)
				{
					// MemoryStream fastest
					using (MemoryStream ms = new MemoryStream())
					{
						ms.Write(buffer, 0, read);
						return new ArraySegment<byte>(ms.GetBuffer(), 0, (int)ms.Length);
					}
				}
				// If no data was read
				// Check whether the connection is still active
				IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
				TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections()
					.Where(x => x.LocalEndPoint.Equals(this._client.Client.LocalEndPoint) 
						&& x.RemoteEndPoint.Equals(this._client.Client.RemoteEndPoint)).ToArray();

				if (tcpConnections == null || tcpConnections.Length == 0 || tcpConnections[0].State != TcpState.Established)
				{
					await DisconnectAsync();
				}

				throw new SocketException();
			}
			catch (IOException)
			{
				// thrown if ReadTimeout (ms) is exceeded
				// see https://docs.microsoft.com/en-us/dotnet/api/system.net.sockets.networkstream.readtimeout?view=net-6.0
				// and https://github.com/dotnet/runtime/issues/24093
				return default;
			}
		}
		private void SendInternal(long contentLength, Stream stream)
		{
			long bytesRemaining = contentLength;
			int bytesRead = 0;

			byte[] buffer = new byte[_settings.StreamBufferSize];

			try
			{
				_sendLock.Wait();

				// Loop until all the data has been sent
				while (bytesRemaining > 0)
				{
					// Read data from the stream
					bytesRead = stream.Read(buffer, 0, buffer.Length);
					if (bytesRead > 0)
					{
						// Send the data over the network stream
						_networkStream.Write(buffer, 0, bytesRead);
						// Subtract the number of bytes read from the number of bytes remaining
						bytesRemaining -= bytesRead;
					}
				}
				// Flush the network stream to ensure all data is sent
				_networkStream.Flush();
				// Raise the DataSent event to notify subscribers that the data was sent
				_events.HandleDataSent(this, new DataSentEventArgs(ServerIpPort, contentLength));
			}
			finally
			{
				// Release the send lock
				_sendLock.Release();
			}
		}

		private async Task SendInternalAsync(long contentLength, Stream stream, CancellationToken token)
		{
			try
			{
				long bytesRemaining = contentLength;
				int bytesRead = 0;
				byte[] buffer = new byte[_settings.StreamBufferSize];

				await _sendLock.WaitAsync(token).ConfigureAwait(false);

				// Loop until all the data has been sent
				while (bytesRemaining > 0)
				{
					// Read data from the stream asynchronously
					bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
					if (bytesRead > 0)
					{
						// Write the data to the network stream asynchronously
						await _networkStream.WriteAsync(buffer, 0, bytesRead, token).ConfigureAwait(false);

						// Subtract the number of bytes read from the number of bytes remaining
						bytesRemaining -= bytesRead;
					}
				}

				await _networkStream.FlushAsync(token).ConfigureAwait(false);
				_events.HandleDataSent(this, new DataSentEventArgs(ServerIpPort, contentLength));
			}
			catch (TaskCanceledException)
			{

			}
			catch (OperationCanceledException)
			{

			}
			finally
			{
				_sendLock.Release();
			}
		}

		private void WaitCompletion()
		{
			try
			{
				_dataReceiver.Wait();
			}
			catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
			{
				Logger?.Invoke("Awaiting a canceled task");
			}
		}

		private async Task WaitCompletionAsync()
		{
			try
			{
				await _dataReceiver;
			}
			catch (TaskCanceledException)
			{
				Logger?.Invoke("Awaiting a canceled task");
			}
		}
	}
}