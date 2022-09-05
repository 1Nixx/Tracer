using Core.Models;
using Serialization.Abstractions;
using System.Net.WebSockets;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Serialization.Json
{
	public class YamlTraceResultSerializer : ITraceResultSerializer
	{
		public void Serialize(TraceResult traceResult, Stream to)
		{
			var result = Mapper.MapToSerializationModel(traceResult);
			
			var serializer = new SerializerBuilder()
				.WithNamingConvention(CamelCaseNamingConvention.Instance)
				.Build();
			var yamlResult = serializer.Serialize(result);

			UnicodeEncoding uniEncoding = new UnicodeEncoding();
			using (MemoryStream ms = new MemoryStream())
			{
				using (var sw = new StreamWriter(ms, uniEncoding))
				{
					sw.Write(yamlResult);
					sw.Flush();
					ms.Seek(0, SeekOrigin.Begin);
				}
			}
		}
	}
}