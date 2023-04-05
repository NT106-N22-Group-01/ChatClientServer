namespace TcpServerClient
{
	public class STcpServerSettings
	{
		public int StreamBufferSize
		{
			get
			{
				return _streamBufferSize;
			}
			set
			{
				if (value < 1) throw new ArgumentException("StreamBufferSize must be one or greater.");
				if (value > 65536) throw new ArgumentException("StreamBufferSize must be less than or equal to 65,536.");
				_streamBufferSize = value;
			}
		}

		private int _streamBufferSize = 65536;

		public STcpServerSettings()
		{

		}
	}
}