using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tracers
{
	internal class MethodTracer : ITracer<MethodTraceResult>
	{
		private readonly MethodTraceResult tracerResult;
		private readonly int skip;

		private List<MethodTracer> _innerMethodTracers { get; set; } 
		private int _nestingСounter { get; set; } = 0;
		private Stopwatch _stopwatch { get; }

		public MethodTracer(int skip = 3)
		{
			_stopwatch = new Stopwatch();
			_innerMethodTracers = new List<MethodTracer>();
			tracerResult = new MethodTraceResult();

			this.skip = skip;
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
				var method = new StackFrame(skip).GetMethod();
				tracerResult.MethodName = method.Name;
				tracerResult.ClassName = method.DeclaringType.Name;
				_stopwatch.Start();
			}
			else if (_nestingСounter == 1)
			{
				var tracer = new MethodTracer(skip + 1);
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
