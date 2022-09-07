using Core.Interfaces;
using Core.Models;
namespace Tests
{
	public class TestMethods
	{
		private ITracer<TraceResult> tracer;

		public TestMethods(ITracer<TraceResult> tracer)
		{
			this.tracer = tracer;
		}

		public void M1()
		{
			tracer.StartTrace();
			Thread.Sleep(100);
			tracer.StopTrace();
		}

		public void M2()
		{
			tracer.StartTrace();
			Thread.Sleep(100);
			M1();
			tracer.StopTrace();
			tracer.StartTrace();
			Thread.Sleep(100);
			M1();
			tracer.StopTrace();
		}

		public void M3()
		{
			tracer.StartTrace();
			M1();
			M2();
			tracer.StopTrace();
		}
	}
}
