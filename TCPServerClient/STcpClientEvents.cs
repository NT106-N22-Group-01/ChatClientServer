using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerClient
{

	public class STcpClientEvents
	{
		#region Public-Members

		/// <summary>
		/// Event to call when the connection is established.
		/// </summary>
		public event EventHandler<ConnectionEventArgs> Connected;

		/// <summary>
		/// Event to call when the connection is destroyed.
		/// </summary>
		public event EventHandler<ConnectionEventArgs> Disconnected;

		/// <summary>
		/// Event to call when byte data has become available from the server.
		/// </summary>
		public event EventHandler<DataReceivedEventArgs> DataReceived;

		/// <summary>
		/// Event to call when byte data has been sent to the server.
		/// </summary>
		public event EventHandler<DataSentEventArgs> DataSent;

		#endregion

		#region Constructors-and-Factories

		/// <summary>
		/// Instantiate the object.
		/// </summary>
		public STcpClientEvents()
		{

		}

		#endregion

		#region Public-Methods

		internal void HandleConnected(object sender, ConnectionEventArgs args)
		{
			Connected?.Invoke(sender, args);
		}

		internal void HandleClientDisconnected(object sender, ConnectionEventArgs args)
		{
			Disconnected?.Invoke(sender, args);
		}

		internal void HandleDataReceived(object sender, DataReceivedEventArgs args)
		{
			DataReceived?.Invoke(sender, args);
		}

		internal void HandleDataSent(object sender, DataSentEventArgs args)
		{
			DataSent?.Invoke(sender, args);
		}

		#endregion
	}
}
