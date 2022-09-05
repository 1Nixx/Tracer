namespace Serialization.Models
{
    public class SerializationTraceResult
	{
		public IReadOnlyList<SerializationThreadTraceResult> Threads { get; set; }
	}
}