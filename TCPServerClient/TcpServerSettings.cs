using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerClient
{
	/// <summary>
	/// SimpleTcp server settings.
	/// </summary>
	public class TcpServerSettings
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
		/// Maximum amount of time to wait before considering a client idle and disconnecting them. 
		/// By default, this value is set to 0, which will never disconnect a client due to inactivity.
		/// The timeout is reset any time a message is received from a client.
		/// For instance, if you set this value to 30000, the client will be disconnected if the server has not received a message from the client within 30 seconds.
		/// </summary>
		public int IdleClientTimeoutMs
		{
			get
			{
				return _idleClientTimeoutMs;
			}
			set
			{
				if (value < 0) throw new ArgumentException("IdleClientTimeoutMs must be zero or greater.");
				_idleClientTimeoutMs = value;
			}
		}

		/// <summary>
		/// Number of milliseconds to wait between each iteration of evaluating connected clients to see if they have exceeded the configured timeout interval.
		/// </summary>
		public int IdleClientEvaluationIntervalMs
		{
			get
			{
				return _idleClientEvaluationIntervalMs;
			}
			set
			{
				if (value < 1) throw new ArgumentOutOfRangeException("IdleClientEvaluationIntervalMs must be one or greater.");
				_idleClientEvaluationIntervalMs = value;
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
		private int _idleClientTimeoutMs = 0;
		private int _idleClientEvaluationIntervalMs = 5000;

		#endregion

		/// <summary>
		/// Instantiate the object.
		/// </summary>
		public TcpServerSettings()
		{

		}
	}
}