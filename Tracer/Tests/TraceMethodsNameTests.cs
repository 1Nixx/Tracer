using Core.Tracers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

[TestFixture]
public class TraceMethodsNameTests
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
	public void SingleMethod_NameM1()
	{
		_testMethods.M1();

		Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo("M1"));
	}

	[Test]
	public void SingleMethod_NameM2()
	{
		_testMethods.M2();

		Assert.Multiple(() =>
		{
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo("M2"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].MethodName, Is.EqualTo("M1"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[1].MethodName, Is.EqualTo("M2"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[1].InnerMethodTraceResults[0].MethodName, Is.EqualTo("M1"));
		});
	}
	
	[Test]
	public void SingleMethod_NameM3()
	{
		_testMethods.M3 ();

		Assert.Multiple(() =>
		{
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].MethodName, Is.EqualTo("M3"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[0].MethodName, Is.EqualTo("M1"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[1].MethodName, Is.EqualTo("M2"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[1].InnerMethodTraceResults[0].MethodName, Is.EqualTo("M1"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[2].MethodName, Is.EqualTo("M2"));
			Assert.That(_tracer.GetTraceResult().ThreadTraceResults[0].MethodTraceResults[0].InnerMethodTraceResults[2].InnerMethodTraceResults[0].MethodName, Is.EqualTo("M1"));
		});
	}
}

