using Common.Helpers;
using System;
using System.Xml.Serialization;

namespace Core.Models.RSS
{
    [XmlRoot(ElementName = "item")]
    public class Item
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "author")]
        public string Author { get; set; }

        [XmlElement(ElementName = "comments")]
        public string CommentsUrl { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PublishDateString { get; set; }

        public DateTime? PublishDate
        {
            get
            {
                return RssFeedHelper.ConvertRssDateStringToDateTime(PublishDateString);
            }
        }
    }
}
