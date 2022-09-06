using System.Xml.Serialization;

namespace Serialization.Models
{
	[XmlRoot("root")]
    public class SerializationTraceResult
	{
		[XmlElement("thread")]
		public List<SerializationThreadTraceResult> Threads { get; set; }
	}
}