using System.Threading;

namespace Core.Models
{
	public class ThreadTraceResult
	{
		public ThreadTraceResult(int threadId, long totalTime, IReadOnlyList<MethodTraceResult> methods)
		{
			Id = threadId;
			ExecutionTime = totalTime;
			MethodTraceResults = methods;
		}

		public int Id { get; }
		public long ExecutionTime { get; }
		public IReadOnlyList<MethodTraceResult> MethodTraceResults { get; }
	}
}