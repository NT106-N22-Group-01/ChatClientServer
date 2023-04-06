using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TcpServerClient;
using Data;

namespace Client
{
	public partial class Client : Form
	{
		private STcpClient _client = null;
		private string _username;
		private delegate void DisplayMessageDelegate(string message);
		private DisplayMessageDelegate _displayMessageDelegate = null;

		public Client()
		{
			InitializeComponent();
		}

		private void Client_Load(object sender, EventArgs e)
		{
			clientDisconnectButton.Enabled = false;
			clientSendButton.Enabled = false;
			_displayMessageDelegate = new DisplayMessageDelegate(DisplayMessage);
		}

		private void clientConnectButton_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(nameTxt.Text))
			{
				MessageBox.Show("Vui lòng nhập Tên tài khoản và thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(serverPortTxt.Text))
			{
				MessageBox.Show("Vui lòng nhập Port và thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (string.IsNullOrEmpty(serverIPTxt.Text))
			{
				MessageBox.Show("Vui lòng nhập IP và thử lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!IPAddress.TryParse(serverIPTxt.Text, out var serverIP))
			{
				MessageBox.Show("Địa chỉ IP không hợp lệ vui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (!Int32.TryParse(serverPortTxt.Text, out var serverPort))
			{
				MessageBox.Show("Số port không hợp lệ vui lòng nhập lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (serverPort < 0 || serverPort > 65535)
			{
				MessageBox.Show("Số port phải lớn hơn 0 và nhỏ hơn hoặc bằng 65535", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			_username = nameTxt.Text;
			_client = new STcpClient($"{serverIP}:{serverPort}");
			try
			{
				_client.Events.Connected += Connected;
				_client.Events.DataReceived += DataReceived;
				_client.Events.ServerDisconnected += ServerDisconnected;
				clientConnectButton.Enabled = false;
				_client.Connect();
			}
			catch (TimeoutException)
			{
				clientConnectButton.Enabled = true;
				MessageBox.Show("Connection Error: TimeOut", "TcpClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void clientDisconnectButton_Click(object sender, EventArgs e)
		{
			Task.Run(() =>
			{
				_client.Disconnect();
				_client.Dispose();
				_client = null;
			});
			clientConnectButton.Enabled = true;
			clientDisconnectButton.Enabled = false;
		}

		private void ServerDisconnected(object sender, ConnectionEventArgs e)
		{
			this.Invoke(_displayMessageDelegate, new Object[] { $"--- Mất kết nối đến server ---" });
			clientConnectButton.Invoke(new Action(() => clientConnectButton.Enabled = true));
			clientDisconnectButton.Invoke(new Action(() => clientDisconnectButton.Enabled = false));
		}

		private void DataReceived(object sender, DataReceivedEventArgs e)
		{
			Object packet = Common.ArraySegmentToObject(e.Data);
			if (packet is UserConnectionPacket ucp)
			{
				if (ucp.IsJoining)
				{
					this.Invoke(_displayMessageDelegate, new Object[] { $"--- {ucp.Username} vừa tham gia vào phòng chat ---" });
				}
				else
				{
					this.Invoke(_displayMessageDelegate, new Object[] { $"--- {ucp.Username} thoát khỏi phòng chat ---" });
				}
			}
			if (packet is ChatPacket chat)
			{
				this.Invoke(_displayMessageDelegate, new Object[] { $"{chat.Username} => {chat.ChatMessage}" });
			}
		}

		private void Connected(object sender, ConnectionEventArgs e)
		{
			this.Invoke(new MethodInvoker(() => clientDisconnectButton.Enabled = true));
			SendJoin();
			this.Invoke(_displayMessageDelegate, new Object[] { $"Đã kết nối đến server" });
		}

		private void SendJoin()
		{
			var ucp = new UserConnectionPacket();
			ucp.Username = _username;
			ucp.IsJoining = true;
			var Data = Common.ObjectToArraySegment(ucp);
			_client.Send(Data.ToArray());
		}

		private void DisplayMessage(string message)
		{
			clientChatView.Text += message + Environment.NewLine;
		}

		private void msgTxt_TextChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(msgTxt.Text))
			{
				clientSendButton.Enabled = false;
			}
			else if (_client.IsConnected)
			{
				clientSendButton.Enabled = true;
			}
		}

		private void clientSendButton_Click(object sender, EventArgs e)
		{
			var Chat = new ChatPacket();
			Chat.Username = _username;
			Chat.ChatMessage = msgTxt.Text;
			var Data = Common.ObjectToArraySegment(Chat);
			try
			{
				_client.Send(Data.ToArray());
			} catch (Exception ex) 
			{
				MessageBox.Show("Send Error: " + ex.Message, "TcpClient", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			msgTxt.Text = string.Empty;
			clientSendButton.Enabled = false;

		}
	}
}
