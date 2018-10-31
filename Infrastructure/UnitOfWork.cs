﻿using System;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RSSReaderContext _context;

        private ICategoryRepository _categoryRepository;
        private IFeedRepository _feedRepository;
        private IPostRepository _postRepository;
        private IUserFeedRepository _userFeedRepository;
        private IUserPostDetailRepository _userPostDetailRepository;

        public UnitOfWork(RSSReaderContext context) => _context = context;

        public ICategoryRepository CategoryRepository => _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);
        public IFeedRepository FeedRepository => _feedRepository = _feedRepository ?? new FeedRepository(_context);
        public IPostRepository PostRepository => _postRepository = _postRepository ?? new PostRepository(_context);
        public IUserFeedRepository UserFeedRepository => _userFeedRepository = _userFeedRepository ?? new UserFeedRepository(_context);
        public IUserPostDetailRepository UserPostDetailRepository => _userPostDetailRepository = _userPostDetailRepository ?? new UserPostDetailRepository(_context);

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
