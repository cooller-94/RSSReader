using AutoMapper;
using Core.Models;
using Core.Models.RSS;
using Core.Services.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SearchFeedManager : ISearchFeedManager
    {
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;
        private readonly IFeedParser<RSSFeed> _feedParser;

        public SearchFeedManager(IFeedService feedService, IMapper mapper, IFeedParser<RSSFeed> feedParser)
        {
            _feedService = feedService;
            _mapper = mapper;
            _feedParser = feedParser;
        }

        public async Task<IEnumerable<FeedDTO>> SearchFeeds(string term)
        {
            if (term == null)
            {
                throw new ArgumentNullException(nameof(term));
            }

            IEnumerable<FeedDTO> feeds = await _feedService.GetAllByTerm(term);

            if (feeds == null || feeds.Count() == 0)
            {
                bool isUrl = Uri.IsWellFormedUriString(term, UriKind.Absolute);

                if (!isUrl)
                {
                    return null;
                }

                FeedDTO feed = await GetFeedByRssUrl(term);

                if (feed != null)
                {
                    return new List<FeedDTO> { feed };
                }

                IEnumerable<WebFeedUrl> webResult = await GetRssUrlsFromWebSite(term);

                if (webResult == null || webResult.Count() == 0)
                {
                    return null;
                }

                return _mapper.Map<IEnumerable<FeedDTO>>(webResult);
            }

            return _mapper.Map<IEnumerable<FeedDTO>>(feeds);
        }

        private async Task<FeedDTO> GetFeedByRssUrl(string url)
        {
            RSSFeed feed = await _feedParser.ParseAsync(url);

            if (feed != null)
            {
                FeedDTO feedDTO = _mapper.Map<FeedDTO>(feed.Channel);
                feedDTO.Link = url;

                return feedDTO;
            }

            return null;
        }

        private async Task<IEnumerable<WebFeedUrl>> GetRssUrlsFromWebSite(string url)
        {
            HtmlWeb web = new HtmlWeb();
            var documentNode = (await web.LoadFromWebAsync(url)).DocumentNode;

            IEnumerable<WebFeedUrl> result = documentNode
                .Descendants("link")
                .Where(d => d.Attributes["rel"]?.Value == "alternate")
                .Select(node => new WebFeedUrl() { Title = node.Attributes["title"]?.Value, Type = node.Attributes["type"]?.Value, Url = node.Attributes["href"]?.Value });

            if (result != null && result.Count() > 0)
            {
                string iconUrl = GetIconUrlFromSite(documentNode);

                foreach (WebFeedUrl webFeedUrl in result)
                {
                    webFeedUrl.IconUrl = iconUrl;
                }
            }

            return result;
        }

        private string GetIconUrlFromSite(HtmlNode documentNode)
        {
            var element = documentNode.SelectSingleNode("/html/head/link[(@rel='shortcut icon' or @rel='icon') and @href]");

            if (element != null)
            {
                return element.Attributes["href"].Value;
            }

            return null;
        }
    }
}
