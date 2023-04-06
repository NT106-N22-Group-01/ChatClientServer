using System;
using System.Buffers;
using System.IO;
using System.Text.Json;

namespace Data
{
	public static class Common {
		public static ArraySegment<byte> ObjectToArraySegment(object obj)
		{
			byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
			return new ArraySegment<byte>(bytes, 0, bytes.Length);
		}

		public static object ArraySegmentToObject(ArraySegment<byte> segment)
		{
			byte[] bytes = segment.ToArray();
			object obj = JsonSerializer.Deserialize<object>(bytes);
			return obj;
		}
	}
}
