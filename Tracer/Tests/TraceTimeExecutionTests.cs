using Core.Tracers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

[TestFixture]
public class TraceTimeExecutionTests
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
	public void OneMethod_TimeNear100()
	{
		_testMethods.M1();

		Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, Is.InRange(100, 200));
	}

	[Test]
	public void OneMethodWithInner_TimeNear400()
	{
		_testMethods.M2();

		var traceResult = _tracer.GetTraceResult();
		Assert.Multiple(() =>
		{
			Assert.That(traceResult.ThreadTraceResults[0].ExecutionTime, Is.InRange(400, 500));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].ExecutionTime, Is.InRange(200, 300));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].ExecutionTime, Is.InRange(100, 200));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[1].ExecutionTime, Is.InRange(200, 300));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[1].InnerMethodTraceResults[0].ExecutionTime, Is.InRange(100, 200));
		});
	}

	[Test]
	public void OneMethodWithManyInners_TimeNear500()
	{
		_testMethods.M3();

		Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].ExecutionTime, Is.InRange(500, 600));
	}

	[Test]
	public void ThwoMethodsInDifferentsThreads_TimeNear100()
	{
		var thread1 = new Thread(_testMethods.M1);
		var thread2 = new Thread(_testMethods.M1);
		thread1.Start();
		thread2.Start();
		thread1.Join();
		thread2.Join();

		var traceResult = _tracer.GetTraceResult();
		Assert.Multiple(() =>
		{
			Assert.That(traceResult.ThreadTraceResults[0].ExecutionTime, Is.InRange(100, 200));
			Assert.That(traceResult.ThreadTraceResults[1].ExecutionTime, Is.InRange(100, 200));
		});
	}
}

