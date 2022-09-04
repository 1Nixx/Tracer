using Core.Interfaces;
using Core.Models;
using Core.Tracers;
using System.Diagnostics;


var tracer = new Tracer();
var test = new Test(tracer);

var t1 = new Thread(() =>
{
	test.M1();
	test.M2();
	test.M3();
});
t1.Start();
t1.Join();
var a = tracer.GetTraceResult();
Console.WriteLine();

class Test
{
	private ITracer<TraceResult> tracer;

	public Test(ITracer<TraceResult> tracer)
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