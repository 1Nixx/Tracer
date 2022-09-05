namespace Serialization.Models
{
    public class SerializationThreadTraceResult
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public IReadOnlyList<SerializationMethodTraceResult> Methods { get; set; }
    }
}
