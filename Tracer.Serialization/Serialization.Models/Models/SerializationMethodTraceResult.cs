namespace Serialization.Models
{
    public class SerializationMethodTraceResult
    {
		public string Name { get; set; }
		public string Class { get; set; }
		public string Time { get; set; }
		public IReadOnlyList<SerializationMethodTraceResult> Methods { get; set; }
	}
}
