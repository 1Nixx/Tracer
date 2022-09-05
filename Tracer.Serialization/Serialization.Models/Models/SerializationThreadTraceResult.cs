using System.Xml.Serialization;

namespace Serialization.Models
{
    public class SerializationThreadTraceResult
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
		[XmlAttribute("time")]
		public string Time { get; set; }
		[XmlElement("method")]
		public List<SerializationMethodTraceResult> Methods { get; set; }
    }
}
