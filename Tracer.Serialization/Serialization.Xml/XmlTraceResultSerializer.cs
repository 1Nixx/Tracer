using Core.Models;
using Serialization.Abstractions;
using Serialization.Models;
using System.Xml.Serialization;

namespace Serialization.Json
{
	public class XmlTraceResultSerializer : ITraceResultSerializer
	{
		public void Serialize(TraceResult traceResult, Stream to)
		{
			var result = Mapper.MapToSerializationModel(traceResult);

			var xmlSerializer = new XmlSerializer(typeof(SerializationTraceResult));

			xmlSerializer.Serialize(to, result);
		}
	}
}