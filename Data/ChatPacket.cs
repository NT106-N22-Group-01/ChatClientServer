namespace Data
{
	[Serializable]
	public class ChatPacket
	{
		public string Username
		{
			get { return _username; }
			set { _username = value; }
		}

		public string ChatMessage
		{
			get { return _message; }
			set { _message = value; }
		}

		private string _username;
		private string _message;

		public ChatPacket()
		{
			_username = "";
			_message = "";
		}

		public ChatPacket(string message, string name)
		{
			_username = name;
			_message = message;
		}
	}
}