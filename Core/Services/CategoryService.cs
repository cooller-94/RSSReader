using AutoMapper;
using Core.Models;
using Core.Services.Interfaces;
using Infrastructure;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Create(CategoryDTO category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            Category dbCategory = await _unitOfWork.CategoryRepository.GetCategoryByTitle(category.Name);

            if (dbCategory == null)
            {
                dbCategory = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepository.Create(dbCategory);
                await _unitOfWork.SaveAsync();
            }
        }

        public Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
