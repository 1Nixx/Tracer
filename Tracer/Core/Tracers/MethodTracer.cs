using Core.Interfaces;
using Core.Models;
using System.Diagnostics;

namespace Core.Tracers
{
	internal class MethodTracer : ITracer<MethodTraceResult>
	{
		private readonly MethodInfo _methodInfo;	
		private readonly int _stackFrameNumber;

		private List<MethodTracer> _innerMethodTracers { get; } 
		private int _nestingСounter { get; set; } = 0;
		private Stopwatch _stopwatch { get; }

		public MethodTracer(int frameNumber = 3)
		{
			_stopwatch = new Stopwatch();
			_innerMethodTracers = new List<MethodTracer>();
			_methodInfo = new MethodInfo();

			_stackFrameNumber = frameNumber;
		}		

		public MethodTraceResult GetTraceResult()
		{
			if (_nestingСounter != 0)
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			_methodInfo.InnerMethodTraceResults = _innerMethodTracers.Select(method => method.GetTraceResult()).ToList();

			var traceResult = new MethodTraceResult(_methodInfo.MethodName, _methodInfo.ClassName, _methodInfo.ExecutionTime, _methodInfo.InnerMethodTraceResults);

			return traceResult;
		}

		public void StartTrace()
		{
			if (_nestingСounter == 0)
			{
				var method = new StackTrace().GetFrame(_stackFrameNumber).GetMethod();

				_methodInfo.MethodName = method.Name;
				_methodInfo.ClassName = method.DeclaringType.Name;

				_stopwatch.Start();
			}
			else if (_nestingСounter == 1)
			{
				var tracer = new MethodTracer(_stackFrameNumber + 1);
				_innerMethodTracers.Add(tracer);
				tracer.StartTrace();
			}
			else if (_nestingСounter > 1)
				_innerMethodTracers.Last().StartTrace();
			else
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			_nestingСounter++;
		}

		public void StopTrace()
		{		
			if (_nestingСounter == 1)
			{
				_stopwatch.Stop();
				_methodInfo.ExecutionTime = _stopwatch.ElapsedMilliseconds;
			}
			else if (_nestingСounter > 1)
				_innerMethodTracers.Last().StopTrace();
			else
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			_nestingСounter--;
		}

		private class MethodInfo
		{
			public string MethodName { get; set; }
			public string ClassName { get; set; }
			public long ExecutionTime { get; set;  }
			public List<MethodTraceResult> InnerMethodTraceResults { get; set; }
		}
	}
}
