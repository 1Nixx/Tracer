using Core.Models;

namespace Serialization.Abstractions
{
	public interface ITraceResultSerializer
	{
		void Serialize(TraceResult traceResult, Stream to);
	}
}
