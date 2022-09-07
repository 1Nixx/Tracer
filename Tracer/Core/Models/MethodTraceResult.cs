using System.Xml.Linq;

namespace Core.Models
{
	public class MethodTraceResult
	{
		public MethodTraceResult(string name, string className, long time, IReadOnlyList<MethodTraceResult> innerMethods)
		{
			MethodName = name;
			ClassName = className;
			ExecutionTime = time;
			InnerMethodTraceResults = innerMethods;
		}

		public string MethodName { get; }
		public string ClassName { get; }
		public long ExecutionTime { get; }
		public IReadOnlyList<MethodTraceResult> InnerMethodTraceResults { get; }
	}
}