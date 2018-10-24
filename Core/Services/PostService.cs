using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostService> _logger;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork, ILogger<PostService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task MarkAsRead(int id)
        {
            Post post = await _unitOfWork.PostRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                _logger.LogWarning($"There is no record with id = {id}");
                return;
            }

            post.IsRead = true;
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<PostDTO>> GetUnreadPostsAsync(int feedId)
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetUnreadPostsByFeedIdAsync(feedId);
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }

        public async Task<IEnumerable<PostDTO>> GetUnreadPostsByCategoryAsync(string category)
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetUnreadPostsByCategoryAsync(category);
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }

        public async Task<IEnumerable<PostDTO>> GetAllUnreadPostsAsync()
        {
            IEnumerable<Post> posts = await _unitOfWork.PostRepository.GetAllUnreadPostsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }
    }
}
