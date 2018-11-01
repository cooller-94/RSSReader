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

        public async Task Create(FeedDTO feed, string category, string userId)
        {
            if (feed == null)
            {
                throw new ArgumentNullException(nameof(feed));
            }

            Feed dbFeed = _mapper.Map<Feed>(feed);

            UserFeed feedUser = new UserFeed();

            if (!string.IsNullOrEmpty(category))
            {
                feedUser.Category = await GetCategoryAsync(category);
            }

            _unitOfWork.FeedRepository.Create(dbFeed);

            feedUser.Feed = dbFeed;
            feedUser.UserId = userId;

            _unitOfWork.UserFeedRepository.Create(feedUser);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Category> GetCategoryAsync(string category)
        {
            Category dbCategory = await _unitOfWork.CategoryRepository.GetCategoryByTitle(category);
            return dbCategory != null ? dbCategory : new Category() { Name = category };
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

        public async Task<IEnumerable<FeedDTO>> GetAllByTerm(string term)
        {
            IEnumerable<Feed> feeds = await _unitOfWork.FeedRepository.GetAllByTerm(term);
            return _mapper.Map<IEnumerable<FeedDTO>>(feeds);
        }

        public async Task<IEnumerable<FeedInformation>> GetAllFeedsAsync(string userId)
        {
            IEnumerable<UserFeed> userFeeds = await _unitOfWork.UserFeedRepository.GetAllFeedUsersAsync(userId);

            IEnumerable<FeedInformation> feedInformation = _mapper.Map<IEnumerable<FeedInformation>>(userFeeds);

            foreach (FeedInformation information in feedInformation)
            {
                information.PostsCount = await _unitOfWork.PostRepository.GetCountUnreadPostsByFeedIdAsync(information.FeedId, userId);
            }

            return feedInformation;
        }
    }
}
