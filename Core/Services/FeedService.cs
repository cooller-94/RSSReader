using AutoMapper;
using Common.Helpers;
using Core.Models;
using Core.Models.RSS;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FeedService : IFeedService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFeedParser<RSSFeed> _feedParser;

        public FeedService(IUnitOfWork unitOfWork, IMapper mapper, IFeedParser<RSSFeed> feedParser)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _feedParser = feedParser;
        }

        public async Task Create(FeedDTO feed)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            Feed dbFeed = _mapper.Map<Feed>(feed);

            if (dbFeed.Category != null)
            {
                Category dbCategory = await _unitOfWork.CategoryRepository.GetCategoryByTitle(dbFeed.Category.Name);

                if (dbCategory != null)
                {
                    dbFeed.Category.CategoryId = dbCategory.CategoryId;
                }
            }

            _unitOfWork.FeedRepository.Create(dbFeed);
            await _unitOfWork.SaveAsync();
        }


        public async Task SyncAllAsync()
        {
            IEnumerable<Feed> dbFeeds = await _unitOfWork.FeedRepository.GetAllAsync();

            IEnumerable<FeedDTO> feeds = _mapper.Map<IEnumerable<FeedDTO>>(dbFeeds);

            List<PostDTO> rssPosts = new List<PostDTO>();

            foreach (FeedDTO feed in feeds)
            {
                IEnumerable<PostDTO> currentItems = await GetItemsAsync(feed.Url);

                if (currentItems != null)
                {
                    foreach (PostDTO item in currentItems)
                    {
                        item.Feed = feed;
                        item.PostHash = RssFeedHelper.EncryptPost(item.Title, item.Description, item.Link);
                        rssPosts.Add(item);
                    }
                }
            }

            string[] urls = rssPosts.Select(u => u.PostHash).ToArray();

            IEnumerable<Post> oldPosts = await _unitOfWork.PostRepository.GetPostsByHashAsync(urls);

            foreach (PostDTO post in rssPosts)
            {
                if (oldPosts.Any(i => i.Link == post.Link))
                {
                    continue;
                }

                Post dbPost = _mapper.Map<Post>(post);
                _unitOfWork.PostRepository.Create(dbPost);
            }

            await _unitOfWork.SaveAsync();
        }


        private async Task<IEnumerable<PostDTO>> GetItemsAsync(string url)
        {
            RSSFeed rssFeed = await _feedParser.ParseAsync(url);

            if (rssFeed == null || rssFeed.Channel == null || rssFeed.Channel.Items == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<PostDTO>>(rssFeed.Channel.Items);
        }


        public Task<IEnumerable<FeedDTO>> GetAllByCategoryAsync(string category)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FeedDTO>> GetAllAsync()
        {
            IEnumerable<Feed> feeds = await _unitOfWork.FeedRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FeedDTO>>(feeds);
        }

        public async Task<RSSFeed> FindFeed(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            RSSFeed feed = await _feedParser.ParseAsync(url);

            return feed;
        }

        public async Task<IEnumerable<FeedInformation>> GetAllFeedsAsync()
        {
            IEnumerable<Feed> feeds = await _unitOfWork.FeedRepository.GetAllFeedsAsync();

            IEnumerable<FeedInformation> feedInformation = _mapper.Map<IEnumerable<FeedInformation>>(feeds);

            foreach (FeedInformation information in feedInformation)
            {
                information.PostsCount = await _unitOfWork.PostRepository.GetCountPostsByFeedIdAsync(information.FeedId);
            }

            return feedInformation;
        }
    }
}
