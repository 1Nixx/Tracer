using Core.Models;
using Serialization.Models;

namespace Serialization
{
	public static class Mapper
	{

		public static SerializationTraceResult MapToSerializationModel(this TraceResult data)
		{
			if (data is null)
				throw new ArgumentNullException(nameof(data));

			var destData = new SerializationTraceResult();

			destData.Threads = new List<SerializationThreadTraceResult>();
			foreach (var item in data.ThreadTraceResults)
			{
				var threadResult = new SerializationThreadTraceResult()
				{
					Id = item.Id,
					Time = $"{item.ExecutionTime}ms"
				};

				threadResult.Methods = new List<SerializationMethodTraceResult>();
				foreach (var methodTrace in item.MethodTraceResults)
					threadResult.Methods.Add(GetSerializeMethodTraceResult(methodTrace));

				destData.Threads.Add(threadResult);
			}
			return destData;
		}

		private static SerializationMethodTraceResult GetSerializeMethodTraceResult(MethodTraceResult methodTrace)
		{
			var data = new SerializationMethodTraceResult()
			{
				Name = methodTrace.MethodName,
				Class = methodTrace.ClassName,
				Time = $"{methodTrace.ExecutionTime}ms"
			};

			data.Methods = new List<SerializationMethodTraceResult>();
			foreach (var innerMethod in methodTrace.InnerMethodTraceResults)
				data.Methods.Add(GetSerializeMethodTraceResult(innerMethod));

			return data;
		}
	}
}
