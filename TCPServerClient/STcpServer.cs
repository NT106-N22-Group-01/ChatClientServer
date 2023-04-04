using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace TcpServerClient
{
	public class STcpServer : IDisposable
	{
		public bool IsListening
		{
			get { return _isListening; }
		}
		public STcpServerSettings Settings
		{
			get { return _settings; }
			set
			{
				if (value == null) _settings = new STcpServerSettings();
				else _settings = value;
			}
		}

		public STcpServerEvents Events
		{
			get { return _events; }
			set 
			{
				if (value == null) _events = new STcpServerEvents();
				else _events = value;
			}
		}
		public int Connections
		{
			get
			{
				return _clients.Count;
			}
		}

		public Action<string> Logger = null;

		#region Private Member
		private readonly string _header = "[TcpServer]";

		private STcpServerEvents _events = new STcpServerEvents();
		private STcpServerSettings _settings = new STcpServerSettings();

		private readonly string _listenerIp = null;
		private readonly IPAddress _ipAddress = null;
		private readonly int _port = 0;

		private readonly ConcurrentDictionary<string, ClientMetadata> _clients = new ConcurrentDictionary<string, ClientMetadata>();
		private readonly ConcurrentDictionary<string, DateTime> _clientsKicked = new ConcurrentDictionary<string, DateTime>();

		private TcpListener _listener;
		private bool _isListening = false;

		private CancellationTokenSource _tokenSource = new CancellationTokenSource();
		private CancellationToken _token;
		private CancellationTokenSource _listenerTokenSource = new CancellationTokenSource();
		private CancellationToken _listenerToken;
		private Task _acceptConnections = null;
		#endregion

		#region Constructor
		public STcpServer(string ipPort)
		{
			if (string.IsNullOrEmpty(ipPort)) throw new ArgumentNullException(nameof(ipPort));

			Util.ParseIpPort(ipPort, out _listenerIp, out _port);

			if (_port < 0) throw new ArgumentException("Port must be zero or greater.");
			if (string.IsNullOrEmpty(_listenerIp))
			{
				_ipAddress = IPAddress.Loopback;
				_listenerIp = _ipAddress.ToString();
			}
			// Listen on any ip address
			else if (_listenerIp == "*" || _listenerIp == "+")
			{
				_ipAddress = IPAddress.Any;
			}
			else
			{
				// If Server use Domain Name
				if (!IPAddress.TryParse(_listenerIp, out _ipAddress))
				{
					_ipAddress = Dns.GetHostEntry(_listenerIp).AddressList[0];
					_listenerIp = _ipAddress.ToString();
				}
			}
			_isListening = false;
			// Cancel token for async
			_token = _tokenSource.Token;
		}
		#endregion

		#region Public Method
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Start() 
		{
			if (_isListening) throw new InvalidOperationException("TcpServer is already running.");

			_listener = new TcpListener(_ipAddress, _port);
			_listener.Server.NoDelay = true;

			_listener.Start();
			_isListening = true;

			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			_listenerTokenSource = new CancellationTokenSource();
			_listenerToken = _listenerTokenSource.Token;

			_acceptConnections = Task.Run(() => AcceptConnections(), _listenerToken);
		}

		public Task StartAsync()
		{
			if (_isListening) throw new InvalidOperationException("");

			_listener = new TcpListener(IPAddress.Any, _port);
			_listener.Server.NoDelay = _settings.NoDelay;
			_listener.Start();
			_isListening = true;

			_tokenSource = new CancellationTokenSource();
			_token = _tokenSource.Token;
			_listenerTokenSource = new CancellationTokenSource();
			_listenerToken = _listenerTokenSource.Token;

			_acceptConnections = Task.Run(() => AcceptConnections(), _listenerToken);
			return _acceptConnections;
		}

		public void Stop()
		{
			if (!_isListening) throw new InvalidOperationException("TcpServer is not running.");

			_isListening = false;
			_listener.Stop();
			_listenerTokenSource.Cancel();
			_acceptConnections.Wait();
			_acceptConnections = null;

		}

		public void Send(string ipPort, long contentLength, Stream stream)
		{
			if (string.IsNullOrEmpty(ipPort)) throw new ArgumentNullException(nameof(ipPort));
			if (contentLength < 1) return;
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new InvalidOperationException("Cannot read from supplied stream.");

			SendInternal(ipPort, contentLength, stream);
		}

		public async Task SendAsync(string ipPort, long contentLength, Stream stream, CancellationToken token = default)
		{
			if (string.IsNullOrEmpty(ipPort)) throw new ArgumentNullException(nameof(ipPort));
			if (contentLength < 1) return;
			if (stream == null) throw new ArgumentNullException(nameof(stream));
			if (!stream.CanRead) throw new InvalidOperationException("Cannot read from supplied stream.");
			if (token == default(CancellationToken)) token = _token;

			await SendInternalAsync(ipPort, contentLength, stream, token).ConfigureAwait(false);
		}

		public void DisconnectClient(string ipPort)
		{
			if (string.IsNullOrEmpty(ipPort)) throw new ArgumentNullException(nameof(ipPort));

			if (!_clients.TryGetValue(ipPort, out ClientMetadata client))
			{
				Logger?.Invoke($"{_header}unable to find client: {ipPort}");
			}
			else
			{
				Logger?.Invoke($"{_header}kicking: {ipPort}");
				_clientsKicked.TryAdd(ipPort, DateTime.Now);
			}

			if (client != null)
			{
				if (!client.TokenSource.IsCancellationRequested)
				{
					client.TokenSource.Cancel();
					Logger?.Invoke($"{_header}requesting disposal of: {ipPort}");
				}

				client.Dispose();
			}
		}
		#endregion

		private bool IsClientConnected(TcpClient client)
		{
			// Check whether the client is connected.
			if (!client.Connected)
			{
				return false;
			}

			// Check whether the client is still active by polling its underlying socket.
			if (client.Client.Poll(0, SelectMode.SelectWrite) && (!client.Client.Poll(0, SelectMode.SelectError)))
			{
				// Try to read a single byte from the client to confirm it is still connected.
				byte[] buffer = new byte[1];
				if (client.Client.Receive(buffer, SocketFlags.Peek) == 0)
				{
					// If no bytes can be read, the client has disconnected.
					return false;
				}

				// If a byte can be read, the client is still connected.
				return true;
			}
			else
			{
				// If the socket is not ready for write or an error condition is detected, the client is disconnected.
				return false;
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposing) return;

			try
			{
				if (_clients != null)
				{
					foreach (KeyValuePair<string, ClientMetadata> curr in _clients)
					{
						curr.Value.Dispose();
						Logger?.Invoke($"{_header}disconnected client: {curr.Key}");
					}

					_clients.Clear();
				}

				_tokenSource?.Cancel();
				_tokenSource?.Dispose();

				_listener?.Server?.Close();
				_listener?.Server?.Dispose();
				_listener?.Stop();
			}
			catch (Exception e)
			{
				Logger?.Invoke($"{_header} dispose exception:{Environment.NewLine}{e}{Environment.NewLine}");
			}

			_isListening = false;

			Logger?.Invoke($"{_header} disposed");
		}

		private async Task AcceptConnections()
		{
			while (!_listenerToken.IsCancellationRequested)
			{
				ClientMetadata client = null;

				try
				{
					// Start listening for incoming connections if not already listening.
					if (!_isListening)
					{
						_listener.Start();
						_isListening = true;
					}

					var tcpClient = await _listener.AcceptTcpClientAsync().ConfigureAwait(false);

					// Extract the client IP address and port number from the remote endpoint.
					var clientIpPort = tcpClient.Client.RemoteEndPoint.ToString();
					Util.ParseIpPort(clientIpPort, out var clientIp, out var clientPort);

					// Create a new ClientMetadata object to store information about the client connection.
					client = new ClientMetadata(tcpClient);
					_clients.TryAdd(clientIpPort, client);

					Logger?.Invoke($"{_header}starting data receiver for: {clientIpPort}");
					_events.HandleClientConnected(this, new ConnectionEventArgs(clientIpPort));

					// Create a new cancellation token that is linked to the client's cancellation token and the server's cancellation token.
					var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(client.Token, _token);

					Task unawaited = Task.Run(() => DataReceiver(client), linkedCts.Token);
				}
				catch (Exception ex) when (ex is TaskCanceledException
									|| ex is OperationCanceledException
									|| ex is ObjectDisposedException
									|| ex is InvalidOperationException)
				{
					// The operation was cancelled, an object was disposed of, or an invalid operation was performed.
					// Stop listening and dispose of the client.
					_isListening = false;
					client?.Dispose();
					Logger?.Invoke($"{_header}stopped listening");
					break;
				}
				catch (Exception ex)
				{
					// An exception occurred while awaiting incoming connections.
					// Dispose of the client and log the exception.
					client?.Dispose();
					Logger?.Invoke($"{_header}exception while awaiting connections: {ex}");
				}
			}
			_isListening = false;
		}

		private async Task DataReceiver(ClientMetadata client)
		{
			// Get the client's IP and port.
			string ipPort = client.IpPort;

			Logger?.Invoke($"{_header}data receiver started for client {ipPort}");

			// Create a cancellation token source that links to the cancellation tokens for the overall task and the client.
			CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_token, client.Token);

			// Loop indefinitely while receiving data from the client.
			while (true)
			{
				try
				{
					if (!IsClientConnected(client.Client))
					{
						Logger?.Invoke($"{_header}client {ipPort} disconnected");
						break;
					}

					if (client.Token.IsCancellationRequested)
					{
						Logger?.Invoke($"{_header}cancellation requested (data receiver for client {ipPort})");
						break;
					}

					var data = await DataReadAsync(client, linkedCts.Token).ConfigureAwait(false);

					// If no data was received, wait for a short time and continue the loop.
					if (data == null)
					{
						await Task.Delay(10, linkedCts.Token).ConfigureAwait(false);
						continue;
					}

					// Create an action to handle the received data and the client's IP and port.
					Action action = () => _events.HandleDataReceived(this, new DataReceivedEventArgs(ipPort, data));

					// If asynchronous data received events are enabled, start a new task to handle the action.
					if (_settings.UseAsyncDataReceivedEvents)
					{
						_ = Task.Run(action, linkedCts.Token);
					}
					// Otherwise, handle the action synchronously.
					else
					{
						action.Invoke();
					}
				}
				catch (IOException)
				{
					Logger?.Invoke($"{_header}data receiver canceled, peer disconnected [{ipPort}]");
					break;
				}
				catch (SocketException)
				{
					Logger?.Invoke($"{_header}data receiver canceled, peer disconnected [{ipPort}]");
					break;
				}
				catch (TaskCanceledException)
				{
					Logger?.Invoke($"{_header}data receiver task canceled [{ipPort}]");
					break;
				}
				catch (ObjectDisposedException)
				{
					Logger?.Invoke($"{_header}data receiver canceled due to disposal [{ipPort}]");
					break;
				}
				catch (Exception e)
				{
					Logger?.Invoke($"{_header}data receiver exception [{ipPort}]:{Environment.NewLine}{e}{Environment.NewLine}");
					break;
				}
			}

			Logger?.Invoke($"{_header}data receiver terminated for client {ipPort}");

			if (_clientsKicked.ContainsKey(ipPort))
			{
				_events.HandleClientDisconnected(this, new ConnectionEventArgs(ipPort));
			}
			else
			{
				_events.HandleClientDisconnected(this, new ConnectionEventArgs(ipPort));
			}


			_clients.TryRemove(ipPort, out _);

			if (client != null) client.Dispose();
		}

		private async Task<ArraySegment<byte>> DataReadAsync(ClientMetadata client, CancellationToken token)
		{
			var buffer = new byte[_settings.StreamBufferSize];
			int read = 0;

			// MemoryStream is fastest
			using (MemoryStream ms = new MemoryStream())
			{
				while (true)
				{
					read = await client.NetworkStream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);

					if (read > 0)
					{
						await ms.WriteAsync(buffer, 0, read, token).ConfigureAwait(false);
						return new ArraySegment<byte>(ms.GetBuffer(), 0, (int)ms.Length);
					}
					else
					{
						throw new SocketException();
					}
				}
			}
		}

		private void SendInternal(string ipPort, long contentLength, Stream stream)
		{
			if (!_clients.TryGetValue(ipPort, out ClientMetadata client)) return;
			// Client didn't connect
			if (client == null) return;

			long bytesRemaining = contentLength;
			int bytesRead = 0;
			var buffer = new byte[_settings.StreamBufferSize];

			try
			{
				client.SendLock.Wait();

				while (bytesRemaining > 0)
				{
					bytesRead = stream.Read(buffer, 0, buffer.Length);
					if (bytesRead > 0)
					{
						client.NetworkStream.Write(buffer, 0, bytesRead);

						bytesRemaining -= bytesRead;
					}
				}

				client.NetworkStream.Flush();
				_events.HandleDataSent(this, new DataSentEventArgs(ipPort, contentLength));
			}
			finally
			{
				if (client != null) client.SendLock.Release();
			}
		}

		private async Task SendInternalAsync(string ipPort, long contentLength, Stream stream, CancellationToken token)
		{
			ClientMetadata client = null;

			try
			{
				if (!_clients.TryGetValue(ipPort, out client)) return;
				// Client didn't connect
				if (client == null) return;

				long bytesRemaining = contentLength;
				int bytesRead = 0;
				byte[] buffer = new byte[_settings.StreamBufferSize];

				await client.SendLock.WaitAsync(token).ConfigureAwait(false);

				while (bytesRemaining > 0)
				{
					bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
					if (bytesRead > 0)
					{
						await client.NetworkStream.WriteAsync(buffer, 0, bytesRead, token).ConfigureAwait(false);

						bytesRemaining -= bytesRead;
					}
				}

				await client.NetworkStream.FlushAsync(token).ConfigureAwait(false);
				_events.HandleDataSent(this, new DataSentEventArgs(ipPort, contentLength));
			}
			catch (TaskCanceledException)
			{

			}
			catch (OperationCanceledException)
			{

			}
			finally
			{
				if (client != null) client.SendLock.Release();
			}
		}
	}
}
