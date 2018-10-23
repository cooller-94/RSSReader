using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Helpers;

namespace Core.Models.RSS
{
    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("language")]
        public string Language { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlElement("pubDate")]
        public string PublishDateString { get; set; }

        public DateTime? PublishDate
        {
            get
            {
                return RssFeedHelper.ConvertRssDateStringToDateTime(PublishDateString);
            }
        }

        [XmlElement("lastBuildDate")]
        public string LastBuildDateString { get; set; }

        public DateTime? LastBuildDate
        {
            get
            {
                return RssFeedHelper.ConvertRssDateStringToDateTime(LastBuildDateString);
            }
        }

        [XmlElement("image")]
        public Image Image { get; set; }

        [XmlElement(ElementName = "item")]
        public List<Item> Items { get; set; }
    }
}
