using System;
using System.Threading.Tasks;
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
        public PostService(IUnitOfWork unitOfWork, ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task MarkAsRead(string hash)
        {
            if (hash == null)
            {
                throw new ArgumentNullException(nameof(hash));
            }

            Post post = await _unitOfWork.PostRepository.GetPostByHashAsync(hash);

            if (post == null)
            {
                _logger.LogWarning($"There is no record with hash = {hash}");
                return;
            }

            post.IsRead = true;
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveAsync();
        }
    }
}
