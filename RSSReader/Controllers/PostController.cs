using System;
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
    [Route("api/posts")]
    [ApiController]
    [Authorize(Policy = "ApiUser")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _caller;

        public PostController(IPostService postService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _postService = postService;
            _mapper = mapper;
            _caller = httpContextAccessor.HttpContext.User;
        }

        [HttpGet]
        [Route("unread/{feedId:int}")]
        public async Task<IEnumerable<PostModel>> GetUneadPosts(int feedId)
        {
            IEnumerable<PostDTO> posts = await _postService.GetUnreadPostsAsync(feedId, _caller.Claims.FirstOrDefault(c => c.Type == "id").Value);

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }

        [HttpGet]
        [Route("unread/categories")]
        public async Task<IEnumerable<PostModel>> GetUnreadPostsByCategory(string category)
        {
            IEnumerable<PostDTO> posts = await _postService.GetUnreadPostsByCategoryAsync(category, _caller.Claims.FirstOrDefault(c => c.Type == "id").Value);

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }

        [HttpGet]
        [Route("latest")]
        public async Task<IEnumerable<PostModel>> GetLatestPosts()
        {
            IEnumerable<PostDTO> posts = await _postService.GetAllUnreadPostsAsync(_caller.Claims.FirstOrDefault(c => c.Type == "id").Value);

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }
    }
}