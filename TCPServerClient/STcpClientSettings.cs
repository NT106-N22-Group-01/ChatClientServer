using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerClient
{
	public class STcpClientSettings
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
				if (value > 65536) throw new ArgumentException("StreamBufferSize must be less than 65,536.");
				_streamBufferSize = value;
			}
		}

		/// <summary>
		/// The number of milliseconds to wait when attempting to connect.
		/// </summary>
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

		/// <summary>
		/// Maximum amount of time to wait before considering the server to be idle and disconnecting from it. 
		/// By default, this value is set to 0, which will never disconnect due to inactivity.
		/// The timeout is reset any time a message is received from the server.
		/// For instance, if you set this value to 30000, the client will disconnect if the server has not sent a message to the client within 30 seconds.
		/// </summary>
		public int IdleServerTimeoutMs
		{
			get
			{
				return _idleServerTimeoutMs;
			}
			set
			{
				if (value < 0) throw new ArgumentException("IdleClientTimeoutMs must be zero or greater.");
				_idleServerTimeoutMs = value;
			}
		}

		/// <summary>
		/// Number of milliseconds to wait between each iteration of evaluating the server connection to see if the configured timeout interval has been exceeded.
		/// </summary>
		public int IdleServerEvaluationIntervalMs
		{
			get
			{
				return _idleServerEvaluationIntervalMs;
			}
			set
			{
				if (value < 1) throw new ArgumentOutOfRangeException("IdleServerEvaluationIntervalMs must be one or greater.");
				_idleServerEvaluationIntervalMs = value;
			}
		}

		/// <summary>
		/// Number of milliseconds to wait between each iteration of evaluating the server connection to see if the connection is lost.
		/// </summary>
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

		/// <summary>
		/// Enable or disable whether the data receiver thread fires the DataReceived event from a background task.
		/// The default is enabled.
		/// </summary>
		public bool UseAsyncDataReceivedEvents = true;

		#endregion

		#region Private-Members

		private bool _noDelay = false;
		private int _streamBufferSize = 65536;
		private int _connectTimeoutMs = 5000;
		private int _idleServerTimeoutMs = 0;
		private int _idleServerEvaluationIntervalMs = 1000;
		private int _connectionLostEvaluationIntervalMs = 200;

		#endregion

		#region Constructors-and-Factories

		/// <summary>
		/// Instantiate the object.
		/// </summary>
		public STcpClientSettings()
		{

		}
	}
}
