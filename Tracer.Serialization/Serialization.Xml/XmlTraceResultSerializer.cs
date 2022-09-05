using Core.Models;
using Serialization.Abstractions;
using Serialization.Models;
using System.Xml;
using System.Xml.Serialization;

namespace Serialization.Json
{
	public class XmlTraceResultSerializer : ITraceResultSerializer
	{
		public void Serialize(TraceResult traceResult, Stream to)
		{
			var result = Mapper.MapToSerializationModel(traceResult);

			var xmlSerializer = new XmlSerializer(typeof(SerializationTraceResult));

			using var xmlWriter = XmlWriter.Create(to, new XmlWriterSettings { Indent = true });
			xmlSerializer.Serialize(xmlWriter, result);
		}
	}
}