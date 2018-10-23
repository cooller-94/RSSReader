using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;


namespace RSSReader.Controllers
{
    [Route("api/[controller]")]
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;
        private readonly IFeedSyncService _syncService;

        public FeedController(IFeedService feedService, IMapper mapper, IFeedSyncService syncService)
        {
            _feedService = feedService;
            _mapper = mapper;
            _syncService = syncService;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFeed(FeedModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedDTO feed = _mapper.Map<FeedDTO>(model);
            await _feedService.Create(feed);

            return Ok("Feed was successfully added.");
        }

        [HttpGet]
        [Route("sync")]
        public async Task<IEnumerable<SyncFeedResultModel>> SyncFeeds()
        {
            IEnumerable<SyncFeedResult> posts = await _syncService.SyncAllAsync();
            return _mapper.Map<IEnumerable<SyncFeedResultModel>>(posts);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IEnumerable<FeedInformationModel>> GetFeedsInformation()
        {
            IEnumerable<FeedInformation> feeds = await _feedService.GetAllFeedsAsync();
            return _mapper.Map<IEnumerable<FeedInformationModel>>(feeds);
        }
    }
}
