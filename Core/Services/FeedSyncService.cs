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
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FeedSyncService : IFeedSyncService
    {
        private readonly IFeedParser<RSSFeed> _feedParser;
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public FeedSyncService(IFeedParser<RSSFeed> feedParser, IFeedService feedService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _feedParser = feedParser;
            _feedService = feedService;
            _mapper = mapper;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<SyncFeedResult>> SyncAllAsync()
        {
            IEnumerable<Feed> feeds = await _unitOfWork.FeedRepository.GetAllFeedsAsync();

            List<PostDTO> rssPosts = new List<PostDTO>();

            List<Task<IEnumerable<PostDTO>>> tasks = new List<Task<IEnumerable<PostDTO>>>();

            foreach (Feed feed in feeds)
            {
                Task<IEnumerable<PostDTO>> task = GetPostsFromRemoteAsync(_mapper.Map<FeedDTO>(feed));
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());

            foreach (Task<IEnumerable<PostDTO>> currentTask in tasks)
            {
                IEnumerable<PostDTO> currentItems = await currentTask;
                rssPosts.AddRange(currentItems);
            }

            string[] hashs = rssPosts.Select(u => u.PostHash).ToArray();

            IEnumerable<Post> oldPosts = await _unitOfWork.PostRepository.GetPostsByHashAsync(hashs);

            List<SyncFeedResult> syncFeedResult = new List<SyncFeedResult>();

            foreach (PostDTO post in rssPosts)
            {
                if (oldPosts.Any(i => i.PostHash == post.PostHash))
                {
                    continue;
                }

                Post dbPost = _mapper.Map<Post>(post);

                _unitOfWork.PostRepository.Create(dbPost);

                SyncFeedResult syncFeed = syncFeedResult.FirstOrDefault(i => i.FeedId == post.Feed.FeedId);

                if (syncFeed == null)
                {
                    syncFeedResult.Add(new SyncFeedResult(post.Feed.FeedId, post.Feed.Category?.Name, post.Feed.Title));
                }
                else
                {
                    syncFeed.PostsCount++;
                }
            }

            await _unitOfWork.SaveAsync();
            return syncFeedResult;
        }

        private async Task<IEnumerable<PostDTO>> GetPostsFromRemoteAsync(FeedDTO feed)
        {
            RSSFeed rssFeed = await _feedParser.ParseAsync(feed.Url);

            if (rssFeed == null || rssFeed.Channel == null || rssFeed.Channel.Items == null)
            {
                return new List<PostDTO>();
            }

            IEnumerable<PostDTO> result = _mapper.Map<IEnumerable<PostDTO>>(rssFeed.Channel.Items);

            foreach (PostDTO item in result)
            {
                item.Feed = _mapper.Map<FeedDTO>(feed);
                item.PostHash = RssFeedHelper.EncryptPost(item.Title, item.Description, item.Link);
                item.DateAdded = DateTime.Now;
            }

            return result;
        }
    }
}
