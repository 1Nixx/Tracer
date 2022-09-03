using Core.Interfaces;
using Core.Models;
using System.Collections.Concurrent;

namespace Core.Tracers
{
	public class Tracer : ITracer<TraceResult>
	{
		private ConcurrentDictionary<int, ThreadTracer> _threadTracers { get; set; }

		public Tracer()
		{
			_threadTracers = new ConcurrentDictionary<int, ThreadTracer>();
		}

		public TraceResult GetTraceResult()
		{
			return new TraceResult() { ThreadTraceResults = _threadTracers.Select(t => t.Value.GetTraceResult()).ToList() };
		}

		public void StartTrace()
		{
			var threadTracer = GetCurrentThreadTracer();

			if (threadTracer is null)
			{
				int currentThreadId = Thread.CurrentThread.ManagedThreadId;
				threadTracer = new ThreadTracer(currentThreadId);
				_threadTracers.TryAdd(currentThreadId, threadTracer);
			}

			threadTracer.StartTrace();
		}

		public void StopTrace()
		{
			var threadTracer = GetCurrentThreadTracer();
			threadTracer.StopTrace();
		}

		private ThreadTracer GetCurrentThreadTracer()
		{
			var threadId = Thread.CurrentThread.ManagedThreadId;
			_threadTracers.TryGetValue(threadId, out ThreadTracer threadTracer);
			return threadTracer;
		}
	}
}
