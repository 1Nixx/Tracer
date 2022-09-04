using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tracers
{
	internal class ThreadTracer : ITracer<ThreadTraceResult>
	{
		private ThreadTraceResult _threadTraceResult { get; set; }

		private List<MethodTracer> _methodTracers { get; set; }

		private MethodTracer _currentMethodTracer { get => _methodTracers.Last(); }

		private int _nestingСounter { get; set; } = 0;

		public ThreadTracer(int threadId)
		{
			_methodTracers = new List<MethodTracer>();
			_threadTraceResult = new ThreadTraceResult() { Id = threadId };
		}	

		public ThreadTraceResult GetTraceResult()
		{
			_threadTraceResult.ExecutionTime = _methodTracers.Select(t => t.GetTraceResult()).Sum(method => method.ExecutionTime);
			_threadTraceResult.MethodTraceResults = _methodTracers.Select(t => t.GetTraceResult()).ToList();
			return _threadTraceResult;
		}

		public void StartTrace()
		{
			if (_nestingСounter == 0)
			{
				var methodTracer = new MethodTracer();
				_methodTracers.Add(methodTracer);		
			}
			_nestingСounter++;

			_currentMethodTracer.StartTrace();
		}

		public void StopTrace()
		{
			_currentMethodTracer.StopTrace();
			_nestingСounter--;
		}
	}
}
