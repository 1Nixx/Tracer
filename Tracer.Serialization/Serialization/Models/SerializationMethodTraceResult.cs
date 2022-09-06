using System.Xml.Serialization;

namespace Serialization.Models
{
    public class SerializationMethodTraceResult
    {
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("class")]
		public string Class { get; set; }
		[XmlAttribute("time")]
		public string Time { get; set; }
		[XmlElement("method")]
		public List<SerializationMethodTraceResult> Methods { get; set; }
	}
}
