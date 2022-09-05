using Core.Models;
using Serialization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
	public static class Mapper
	{

		public static SerializationTraceResult MapToSerializationModel(this TraceResult data)
		{
			if (data is null)
				throw new ArgumentNullException(nameof(data));

			var destData = new SerializationTraceResult();
			var threadList = new List<SerializationThreadTraceResult>();
			foreach (var item in data.ThreadTraceResults)
			{
				var threadResult = new SerializationThreadTraceResult()
				{
					Id = item.Id,
					Time = $"{item.ExecutionTime}ms"
				};

				var methodList = new List<SerializationMethodTraceResult>();
				foreach (var methodTrace in item.MethodTraceResults)
					methodList.Add(GetSerMethotTraceresult(methodTrace));

				threadResult.Methods = methodList;
				threadList.Add(threadResult);
			}
			destData.Threads = threadList;
			return destData;
		}

		private static SerializationMethodTraceResult GetSerMethotTraceresult(MethodTraceResult methodTrace)
		{
			var data = new SerializationMethodTraceResult()
			{

				Name = methodTrace.MethodName,
				Class = methodTrace.ClassName,
				Time = $"{methodTrace.ExecutionTime}ms"
			};

			var list = new List<SerializationMethodTraceResult>();
			foreach (var innerMethod in methodTrace.InnerMethodTraceResults)
				list.Add(GetSerMethotTraceresult(innerMethod));

			data.Methods = list;
			return data;
		}
	}
}
