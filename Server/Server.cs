using System;
using System.Windows.Forms;
using TcpServerClient;
using Data;
using System.Collections.Concurrent;

namespace Server
{
	public partial class Server : Form
	{
		private STcpServer _server = null;
		private readonly ConcurrentDictionary<string, string> _usernames = new ConcurrentDictionary<string, string>();

		private delegate void UpdateStatusDelegate(string status);
		private UpdateStatusDelegate _updateStatusDelegate;

		public Server()
		{
			InitializeComponent();
			serverStopButton.Enabled = false;
		}

		private void serverStartButton_Click(object sender, EventArgs e)
		{
			serverChatView.Text = String.Empty;
			if (string.IsNullOrEmpty(serverPortTxt.Text))
			{
				MessageBox.Show("Vui lòng nhập Port và thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!Int32.TryParse(serverPortTxt.Text, out var portNumber))
			{
				MessageBox.Show("Số port không hợp lệ vui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (portNumber < 0 || portNumber > 65535)
			{
				MessageBox.Show("Số port phải lớn hơn 0 và nhỏ hơn hoặc bằng 65535", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			_server = new STcpServer($"*:{portNumber}");
			try
			{
				_server.StartAsync();
				serverStartButton.Enabled = false;
				serverStopButton.Enabled = true;
				_updateStatusDelegate = new UpdateStatusDelegate(this.UpdateStatus);
				this?.Invoke(_updateStatusDelegate, new object[] { $"Đang nghe tại port {portNumber}" });
			}
			catch (Exception ex)
			{
				MessageBox.Show("Load Error: " + ex.Message, "TcpServer", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void serverStopButton_Click(object sender, EventArgs e)
		{
			_server.Stop();
			_server = null;
			serverStartButton.Enabled = true;
			serverStopButton.Enabled = false;
			this.Invoke(_updateStatusDelegate, new object[] { "Dừng server" });
		}

		private void DataReceived(object sender, DataReceivedEventArgs e)
		{
			Object packet = Common.ArraySegmentToObject(e.Data);
			if (packet is UserConnectionPacket ucp)
			{
				_usernames.TryAdd(e.IpPort, ucp.Username);
				SendToClient(e.Data);
				this.Invoke(_updateStatusDelegate, new Object[] { $"---- {ucp.Username} vừa tham gia vào phòng chat ---" });
			}
			if (packet is ChatPacket chat)
			{
				SendToClient(e.Data);
				this.Invoke(_updateStatusDelegate, new Object[] { $"{chat.Username} => {chat.ChatMessage}" });
			}	
		}

		private void ClientDisconnected(object sender, ConnectionEventArgs e) 
		{
			if (!_usernames.TryGetValue(e.IpPort, out var username)) return;

			UserConnectionPacket ucp = new UserConnectionPacket();

			ucp.Username = username;
			ucp.IsJoining = false;
			var data = Common.ObjectToArraySegment(ucp);
			SendToClient(data);
			this.Invoke(_updateStatusDelegate, new Object[] { $"--- {username} thoát khỏi phòng chat ---" });
		}

		private void SendToClient(ArraySegment<byte> data)
		{
			try
			{
				foreach (string ipPort in _server.GetClients())
				{
					_ = _server.SendAsync(ipPort, data.ToArray());
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Send Error: " + ex.Message, "Tcp Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void UpdateStatus(string status)
		{
			serverChatView.Text += status + Environment.NewLine;
		}
	}
}
