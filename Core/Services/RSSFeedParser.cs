using Core.CustomExceptions;
using Core.Models.RSS;
using Core.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Core.Services
{
    public class RSSFeedParser : IFeedParser<RSSFeed>
    {
        private readonly ILogger<RSSFeedParser> _logger;

        public RSSFeedParser(ILogger<RSSFeedParser> logger)
        {
            _logger = logger;
        }

        public async Task<RSSFeed> ParseAsync(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            XmlSerializer serializer = new XmlSerializer(typeof(RSSFeed));

            using (WebClient client = new WebClient())
            {
                string dataString = await client.DownloadStringTaskAsync(url);

                if (string.IsNullOrEmpty(dataString))
                {
                    return null;
                }

                using (TextReader reader = new StringReader(dataString))
                {
                    RSSFeed result = (RSSFeed)serializer.Deserialize(reader);
                    return result;
                }
            }
        }
    }
}
