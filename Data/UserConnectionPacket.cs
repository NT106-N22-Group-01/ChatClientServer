﻿namespace Data
{
	[Serializable]
	public class UserConnectionPacket
	{
		public string Username { get; set; }
		public bool IsJoining { get; set; }
	}
}
