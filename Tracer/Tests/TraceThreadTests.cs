using Core.Interfaces;
using Core.Models;
using Core.Tracers;

namespace Tests;

[TestFixture]
public class TraceThreadTests
{
	private TestMethods _testMethods;
	private MainTracer _tracer;

	[SetUp]
    public void Setup()
    {
		_tracer = new MainTracer();
		_testMethods = new TestMethods(_tracer);
    }

    [Test]
    public void SingleMethod_OneThread()
    {
		_testMethods.M1();

        Assert.That(_tracer.GetTraceResult().ThreadTraceResults.Count, Is.EqualTo(1));
    }

	[Test]
	public void TwoMethods_OneThread()
	{
		_testMethods.M1();
		_testMethods.M1();

		Assert.That(_tracer.GetTraceResult().ThreadTraceResults.Count, Is.EqualTo(1));
	}

	[Test]
	public void TwoDifferentMethods_OneThread()
	{
		_testMethods.M1();
		_testMethods.M2();
		
		Assert.That(_tracer.GetTraceResult().ThreadTraceResults.Count, Is.EqualTo(1));
	}

	[Test]
	public void TwoMethods_TwoThreads()
	{
		var thread1 = new Thread(_testMethods.M1);
		var thread2 = new Thread(_testMethods.M1);
		thread1.Start();
		thread2.Start();
		thread1.Join();
		thread2.Join();

		Assert.That(_tracer.GetTraceResult().ThreadTraceResults.Count, Is.EqualTo(2));
	}
}