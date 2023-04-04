namespace TcpServerClient
{
	public class ConnectionEventArgs : EventArgs
	{
		internal ConnectionEventArgs(string ipPort)
		{
			IpPort = ipPort;
		}

		public string IpPort { get; }
	}

	public class DataSentEventArgs : EventArgs
	{
		internal DataSentEventArgs(string ipPort, long bytesSent)
		{
			IpPort = ipPort;
			BytesSent = bytesSent;
		}

		public string IpPort { get; }

		public long BytesSent { get; }
	}

	public class DataReceivedEventArgs : EventArgs
	{
		internal DataReceivedEventArgs(string ipPort, ArraySegment<byte> data)
		{
			IpPort = ipPort;
			Data = data;
		}

		public string IpPort { get; }

		public ArraySegment<byte> Data { get; }
	}
}
