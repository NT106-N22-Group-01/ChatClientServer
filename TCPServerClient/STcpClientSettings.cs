namespace TcpServerClient
{
	public class STcpClientSettings
	{
		public bool NoDelay
		{
			get
			{
				return _noDelay;
			}
			set
			{
				_noDelay = value;
			}
		}

		public int StreamBufferSize
		{
			get
			{
				return _streamBufferSize;
			}
			set
			{
				if (value < 1) throw new ArgumentException("StreamBufferSize must be one or greater.");
				if (value > 65536) throw new ArgumentException("StreamBufferSize must be less than 65,536.");
				_streamBufferSize = value;
			}
		}

		public int ConnectTimeoutMs
		{
			get
			{
				return _connectTimeoutMs;
			}
			set
			{
				if (value < 1) throw new ArgumentException("ConnectTimeoutMs must be greater than zero.");
				_connectTimeoutMs = value;
			}
		}

		public int ConnectionLostEvaluationIntervalMs
		{
			get
			{
				return _connectionLostEvaluationIntervalMs;
			}
			set
			{
				if (value < 1) throw new ArgumentOutOfRangeException("ConnectionLostEvaluationIntervalMs must be one or greater.");
				_connectionLostEvaluationIntervalMs = value;
			}
		}


		/// </summary>


		private int _streamBufferSize = 65536;
		private int _connectTimeoutMs = 5000;
		private int _connectionLostEvaluationIntervalMs = 200;


		public STcpClientSettings()
		{

		}
	}
}
