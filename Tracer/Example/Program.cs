using Core.Interfaces;
using Core.Models;
using Core.Tracers;
using Serialization.Abstractions;
using System.Reflection;
using Tests;

var tracer = new Tracer();
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

var pluginList = new List<ITraceResultSerializer>();
var pluginPath = "../../../Plugins/";

LoadPlugins();

for (int i = 1; i <= pluginList.Count; i++)
{
	using var fileStream = new FileStream(pluginPath + $"test{i}.txt", FileMode.Create, FileAccess.Write);
	pluginList[i - 1].Serialize(traceResult, fileStream);
}


void LoadPlugins()
{
	pluginList.Clear();

	DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
	if (!pluginDirectory.Exists)
		pluginDirectory.Create();

	//берем из директории все файлы с расширением .dll      
	var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
	foreach (var file in pluginFiles)
	{
		//загружаем сборку
		Assembly asm = Assembly.LoadFrom(file);
		//ищем типы, имплементирующие наш интерфейс IPlugin,
		//чтобы не захватить лишнего
		var types = asm.GetTypes().
						Where(t => t.GetInterfaces().
						Where(i => i.FullName == typeof(ITraceResultSerializer).FullName).Any());

		//заполняем экземплярами полученных типов коллекцию плагинов
		foreach (var type in types)
		{
			var plugin = asm.CreateInstance(type.FullName) as ITraceResultSerializer;
			pluginList.Add(plugin);
		}
	}
}