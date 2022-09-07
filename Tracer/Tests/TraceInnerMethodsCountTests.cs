using Core.Tracers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

[TestFixture]
public class TraceInnerMethodsCountTests
{
	private TestMethods _testMethods;
	private Tracer _tracer;

	[SetUp]
	public void Setup()
	{
		_tracer = new Tracer();
		_testMethods = new TestMethods(_tracer);
	}

	[Test]
	public void MethodWithoutInnerMethods()
	{
		_testMethods.M1();

		var traceResult = _tracer.GetTraceResult();
		Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults.Count, Is.EqualTo(0));
	}

	[Test]
	public void MethodWithOneInnerMethod()
	{
		_testMethods.M2();

		var traceResult = _tracer.GetTraceResult();
		Assert.Multiple(() =>
		{
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults.Count, Is.EqualTo(2));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults.Count, Is.EqualTo(1));
		});
	}

	[Test]
	public void MethodWithManyInnerMethods()
	{
		_testMethods.M3();

		var traceResult = _tracer.GetTraceResult();
		Assert.Multiple(() =>
		{
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults.Count, Is.EqualTo(3));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].InnerMethodTraceResults.Count, Is.EqualTo(0));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[1].InnerMethodTraceResults.Count, Is.EqualTo(1));
			Assert.That(traceResult.ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[2].InnerMethodTraceResults.Count, Is.EqualTo(1));
		});
	}
}