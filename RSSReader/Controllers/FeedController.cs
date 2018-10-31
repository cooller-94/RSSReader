using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;


namespace RSSReader.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "ApiUser")]
    public class FeedController : Controller
    {
        private readonly IFeedService _feedService;
        private readonly IMapper _mapper;
        private readonly IFeedSyncService _syncService;
        private readonly ClaimsPrincipal _caller;

        public FeedController(IFeedService feedService, IMapper mapper, IFeedSyncService syncService, IHttpContextAccessor httpContextAccessor)
        {
            _feedService = feedService;
            _mapper = mapper;
            _syncService = syncService;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFeed([FromBody]FeedModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            FeedDTO feed = _mapper.Map<FeedDTO>(model);
            await _feedService.Create(feed, model.Category, _caller.Claims.FirstOrDefault(c => c.Type == "id").Value);

            return Ok("Feed was successfully added.");
        }

        [HttpGet]
        [Route("sync")]
        public async Task<IEnumerable<SyncFeedResultModel>> SyncFeeds()
        {
            IEnumerable<SyncFeedResult> posts = await _syncService.SyncAllAsync(_caller.Claims.FirstOrDefault(c => c.Type == "id").Value);
            return _mapper.Map<IEnumerable<SyncFeedResultModel>>(posts);
        }

        [HttpGet]
        [Route("info")]
        public async Task<IEnumerable<FeedInformationModel>> GetFeedsInformation()
        {
            IEnumerable<FeedInformation> feeds = await _feedService.GetAllFeedsAsync(_caller.Claims.FirstOrDefault(c => c.Type == "id").Value);
            return _mapper.Map<IEnumerable<FeedInformationModel>>(feeds);
        }
    }
}
