﻿using Core.Models;
using Serialization.Models;

namespace Serialization
{
	public static class Mapper
	{
		public static SerializationTraceResult MapToSerializationModel(this TraceResult traceResult)
		{
			if (traceResult is null)
				throw new ArgumentNullException(nameof(traceResult));

			var serializedTraceResult = new SerializationTraceResult();

			serializedTraceResult.Threads = new List<SerializationThreadTraceResult>();
			foreach (var item in traceResult.ThreadTraceResults)
			{
				var threadResult = new SerializationThreadTraceResult()
				{
					Id = item.Id,
					Time = $"{item.ExecutionTime}ms"
				};

				threadResult.Methods = new List<SerializationMethodTraceResult>();
				foreach (var methodTrace in item.MethodTraceResults)
					threadResult.Methods.Add(GetSerializeMethodTraceResult(methodTrace));

				serializedTraceResult.Threads.Add(threadResult);
			}
			return serializedTraceResult;
		}

		private static SerializationMethodTraceResult GetSerializeMethodTraceResult(MethodTraceResult methodTrace)
		{
			var newMethodTrace = new SerializationMethodTraceResult()
			{
				Name = methodTrace.MethodName,
				Class = methodTrace.ClassName,
				Time = $"{methodTrace.ExecutionTime}ms"
			};

			newMethodTrace.Methods = new List<SerializationMethodTraceResult>();
			foreach (var innerMethod in methodTrace.InnerMethodTraceResults)
				newMethodTrace.Methods.Add(GetSerializeMethodTraceResult(innerMethod));

			return newMethodTrace;
		}
	}
}
