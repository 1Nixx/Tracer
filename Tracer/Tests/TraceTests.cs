using Core.Interfaces;
using Core.Models;
using Core.Tracers;

namespace Tests;

[TestFixture]
public class TraceTests
{
	private ITracer<TraceResult> _tracer;

	private void M1()
	{
		_tracer.StartTrace();
		Thread.Sleep(100);
		_tracer.StopTrace();
	}

	private void M2()
	{
		_tracer.StartTrace();
		Thread.Sleep(100);
		M1();
		_tracer.StopTrace();
	}

	[SetUp]
    public void Setup()
    {
		_tracer = new Tracer();
    }

    [Test]
    public void Test1()
    {
		M1();

        Assert.AreEqual(1, _tracer.GetTraceResult().ThreadTraceResults.Count);
		Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, 100);
    }

	[Test]
	public void Test2()
	{
		M1();
		M1();

		Assert.AreEqual(1, _tracer.GetTraceResult().ThreadTraceResults.Count);
		Assert.AreEqual(2, _tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults.Count);
		Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, 200);
	}

	[Test]
	public void Test3()
	{
		M1();
		M2();
		
		Assert.AreEqual(1, _tracer.GetTraceResult().ThreadTraceResults.Count);
		Assert.AreEqual(2, _tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults.Count);
		Assert.AreEqual(1, _tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[1].InnerMethodTraceResults.Count);
		Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, 300);
	}

	[Test]
	public void Test4()
	{
		var thread1 = new Thread(M1);
		var thread2 = new Thread(M1);
		thread1.Start();
		thread2.Start();
		thread1.Join();
		thread2.Join();

		Assert.AreEqual(2, _tracer.GetTraceResult().ThreadTraceResults.Count);
		Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, 100);
		Assert.GreaterOrEqual(_tracer.GetTraceResult().ThreadTraceResults[1].ExecutionTime, 100);
	}
}