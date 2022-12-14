using Core.Interfaces;
using Core.Models;

namespace Core.Tracers
{
	internal class ThreadTracer : ITracer<ThreadTraceResult>
	{
		private List<MethodTracer> _methodTracers { get; }

		private MethodTracer _currentMethodTracer { get => _methodTracers.Last(); }

		private int _nestingСounter { get; set; } = 0;
		private readonly int _threadId;

		public ThreadTracer(int threadId)
		{
			_methodTracers = new List<MethodTracer>();
			_threadId = threadId;
		}	

		public ThreadTraceResult GetTraceResult()
		{
			if (_nestingСounter != 0)
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			var threadTrace = new ThreadTraceResult(_threadId, 
				_methodTracers.Select(t => t.GetTraceResult()).Sum(method => method.ExecutionTime),
				_methodTracers.Select(t => t.GetTraceResult()).ToList());
			return threadTrace;
		}

		public void StartTrace()
		{
			if (_nestingСounter == 0)
			{
				var methodTracer = new MethodTracer();
				_methodTracers.Add(methodTracer);
			}
			else if (_nestingСounter < 0)
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			_nestingСounter++;

			_currentMethodTracer.StartTrace();
		}

		public void StopTrace()
		{
			if (_nestingСounter <= 0)
				throw new InvalidOperationException("Different amount of StartTrace and StopTrace");

			_currentMethodTracer.StopTrace();
			_nestingСounter--;
		}
	}
}
