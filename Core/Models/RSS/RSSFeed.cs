using System.Xml.Serialization;

namespace Core.Models.RSS
{
    [XmlRoot(ElementName = "rss")]
    public class RSSFeed
    {
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }
}
