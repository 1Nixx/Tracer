using Core.Interfaces;
using Core.Models;
using System.Diagnostics;

namespace Core.Tracers
{
	internal class MethodTracer : ITracer<MethodTraceResult>
	{
		private MethodTraceResult tracerResult;
		private readonly int _stackFrameNumber;

		private List<MethodTracer> _innerMethodTracers { get; set; } 
		private int _nestingСounter { get; set; } = 0;
		private Stopwatch _stopwatch { get; }

		public MethodTracer(int frameNumber = 3)
		{
			_stopwatch = new Stopwatch();
			_innerMethodTracers = new List<MethodTracer>();
			tracerResult = new MethodTraceResult();

			_stackFrameNumber = frameNumber;
		}		

		public MethodTraceResult GetTraceResult()
		{
			tracerResult.InnerMethodTraceResults = _innerMethodTracers.Select(method => method.GetTraceResult()).ToList();
			return tracerResult;
		}

		public void StartTrace()
		{
			if (_nestingСounter == 0)
			{
				var method = new StackTrace().GetFrame(_stackFrameNumber).GetMethod();
				tracerResult.MethodName = method.Name;
				tracerResult.ClassName = method.DeclaringType.Name;
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

			_nestingСounter++;
		}

		public void StopTrace()
		{		
			if (_nestingСounter == 1)
			{
				_stopwatch.Stop();
				tracerResult.ExecutionTime = _stopwatch.ElapsedMilliseconds;
			}
			else if (_nestingСounter > 1)
				_innerMethodTracers.Last().StopTrace();

			_nestingСounter--;
		}
	}
}
