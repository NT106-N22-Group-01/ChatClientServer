namespace TcpServerClient
{
	public class STcpServerEvents
	{
		public event EventHandler<ConnectionEventArgs> ClientConnected;

		public event EventHandler<ConnectionEventArgs> ClientDisconnected;

		public event EventHandler<DataReceivedEventArgs> DataReceived;

		public event EventHandler<DataSentEventArgs> DataSent;


		public STcpServerEvents()
		{

		}

		internal void HandleClientConnected(object sender, ConnectionEventArgs args)
		{
			ClientConnected?.Invoke(sender, args);
		}

		internal void HandleClientDisconnected(object sender, ConnectionEventArgs args)
		{
			ClientDisconnected?.Invoke(sender, args);
		}

		internal void HandleDataReceived(object sender, DataReceivedEventArgs args)
		{
			DataReceived?.Invoke(sender, args);
		}

		internal void HandleDataSent(object sender, DataSentEventArgs args)
		{
			DataSent?.Invoke(sender, args);
		}
	}
}
