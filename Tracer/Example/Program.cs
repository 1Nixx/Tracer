using Core.Tracers;
using Tests;
using Tracer.Serialization;

var tracer = new MainTracer();
var test = new TestMethods(tracer);

var t1 = new Thread(() =>
{
	test.M1();
	test.M2();
	test.M3();
});
t1.Start();

var t2 = new Thread(() =>
{
	test.M1();
	test.M2();
	test.M3();
});
t2.Start();

t1.Join();
t2.Join();

var traceResult = tracer.GetTraceResult();

var pluginPath = "../../../Plugins/";

var pluginList = PluginLoader.Load(pluginPath);

for (int i = 1; i <= pluginList.Count; i++)
{
	using var fileStream = new FileStream(pluginPath + $"test{i}.txt", FileMode.Create, FileAccess.Write);
	pluginList[i - 1].Serialize(traceResult, fileStream);
}