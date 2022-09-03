using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
	internal interface ITracer
	{
		void StartTrace();

		void StopTrace();

		TraceResult GetTraceResult();
	}
}
