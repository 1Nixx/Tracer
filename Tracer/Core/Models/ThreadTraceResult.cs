namespace Core.Models
{
	public class ThreadTraceResult
	{
		public int Id { get; set; }
		public long ExecutionTime { get; set; }
		public IReadOnlyList<MethodTraceResult> MethodTraceResults { get; set; }
	}
}