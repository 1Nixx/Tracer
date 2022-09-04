using Core.Models;
using Serialization.Abstractions;
using System.Text.Json;

namespace Serialization.Json
{
	public class JsonTraceResultSerializer : ITraceResultSerializer
	{
		public void Serialize(TraceResult traceResult, Stream to)
		{
			JsonSerializer.Serialize(to, traceResult);
		}
	}
}