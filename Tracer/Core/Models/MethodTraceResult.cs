namespace Core.Models
{
	public class MethodTraceResult
	{
		public string MethodName { get; set; }
		public string ClassName { get; set; }
		public long ExecutionTime { get; set; }
		public IReadOnlyList<MethodTraceResult> InnerMethodTraceResults { get; set; }
	}
}