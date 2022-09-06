namespace Core.Interfaces
{
	public interface ITracer<T>
	{
		void StartTrace();

		void StopTrace();

		T GetTraceResult();
	}
}
