using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerClient
{
	/// <summary>
	/// TcpServer settings.
	/// </summary>
	public class STcpServerSettings
	{
		#region Public-Members

		/// <summary>
		/// Gets or sets a value that disables a delay when send or receive buffers are not full.
		/// true if the delay is disabled; otherwise, false. The default value is false.
		/// </summary>
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

		/// <summary>
		/// Buffer size to use while interacting with streams. 
		/// </summary>
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

		/// <summary>
		/// Enable or disable whether the data receiver thread fires the DataReceived event from a background task.
		/// The default is enabled.
		/// </summary>
		public bool UseAsyncDataReceivedEvents = true;

		#endregion

		#region Private-Members

		private bool _noDelay = false;
		private int _streamBufferSize = 65536;

		#endregion

		/// <summary>
		/// Instantiate the object.
		/// </summary>
		public STcpServerSettings()
		{

		}
	}
}