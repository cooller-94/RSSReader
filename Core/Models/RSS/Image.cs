using System.Xml.Serialization;

namespace Core.Models.RSS
{
    [XmlRoot(ElementName = "image")]
    public class Image
    {
        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "width")]
        public double? Width { get; set; }

        [XmlElement(ElementName = "height")]
        public double Height { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
    }
}
