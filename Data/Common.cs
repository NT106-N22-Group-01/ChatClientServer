using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Data
{
	public class ChatPacketSerializationBinder : ISerializationBinder
	{
		public Type BindToType(string assemblyName, string typeName)
		{
			if (typeName == "Data.ChatPacket")
			{
				return typeof(ChatPacket);
			}
			else if (typeName == "Data.UserConnectionPacket")
			{
				return typeof(UserConnectionPacket);
			}
			return null;
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			if (serializedType == typeof(ChatPacket))
			{
				typeName = "Data.ChatPacket";
			}
			else if (serializedType == typeof(UserConnectionPacket))
			{
				typeName = "Data.UserConnectionPacket";
			}
			else
			{
				typeName = null;
			}
		}
	}

	public static class Common {
		public static ArraySegment<byte> ObjectToArraySegment(object obj)
		{
			var serializerSettings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
				SerializationBinder = new ChatPacketSerializationBinder()
			};
			var json = JsonConvert.SerializeObject(obj, serializerSettings);
			var bytes = Encoding.UTF8.GetBytes(json);
			return new ArraySegment<byte>(bytes, 0, bytes.Length);
		}

		public static object ArraySegmentToObject(ArraySegment<byte> segment)
		{
			var serializerSettings = new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.Objects,
				SerializationBinder = new ChatPacketSerializationBinder()
			};
			var json = Encoding.UTF8.GetString(segment.Array, segment.Offset, segment.Count);
			return JsonConvert.DeserializeObject(json, serializerSettings);
		}
	}
}
