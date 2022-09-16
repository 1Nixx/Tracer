using Serialization.Abstractions;
using System.Reflection;

namespace Tracer.Serialization
{
	public static class PluginLoader
	{

		public static List<ITraceResultSerializer> Load(string directory)
		{
			var result = new List<ITraceResultSerializer>();

			DirectoryInfo pluginDirectory = new DirectoryInfo(directory);
			if (!pluginDirectory.Exists)
				pluginDirectory.Create();
   
			var pluginFiles = Directory.GetFiles(directory, "*.dll");
			foreach (var file in pluginFiles)
			{
				Assembly asm = Assembly.LoadFrom(file);

				var types = asm.GetTypes().
								Where(t => t.GetInterfaces().
								Where(i => i.FullName == typeof(ITraceResultSerializer).FullName).Any());

				foreach (var type in types)
				{
					var plugin = asm.CreateInstance(type.FullName) as ITraceResultSerializer;
					result.Add(plugin);
				}
			}

			return result;
		}

	}
}