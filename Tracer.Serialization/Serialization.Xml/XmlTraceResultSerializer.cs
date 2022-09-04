using Core.Models;
using Serialization.Abstractions;
using System.Text.Json;

namespace Serialization.Json
{
	public class XmlTraceResultSerializer : ITraceResultSerializer
	{
		public void Serialize(TraceResult traceResult, Stream to)
		{
			JsonSerializer.Serialize(to, traceResult);
		}
	}
}