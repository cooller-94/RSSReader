using System;
using System.Collections.Generic;
using System.Linq;
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

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("unread/{feedId:int}")]
        public async Task<IEnumerable<PostModel>> GetUneadPosts(int feedId)
        {
            IEnumerable<PostDTO> posts = await _postService.GetUnreadPostsAsync(feedId);

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }

        [HttpGet]
        [Route("unread/categories")]
        public async Task<IEnumerable<PostModel>> GetUnreadPostsByCategory(string category)
        {
            IEnumerable<PostDTO> posts = await _postService.GetUnreadPostsByCategoryAsync(category);

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }

        [HttpGet]
        [Route("latest")]
        public async Task<IEnumerable<PostModel>> GetLatestPosts()
        {
            IEnumerable<PostDTO> posts = await _postService.GetAllUnreadPostsAsync();

            IEnumerable<PostModel> postsViewModel = _mapper.Map<IEnumerable<PostModel>>(posts);

            return postsViewModel;
        }
    }
}