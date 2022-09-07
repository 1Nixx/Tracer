using System.Threading;

namespace Core.Models
{
    public class TraceResult
    {
		public TraceResult(IReadOnlyList<ThreadTraceResult> threads)
		{
			ThreadTraceResults = threads;
		}

		public IReadOnlyList<ThreadTraceResult> ThreadTraceResults { get; }
    }
}