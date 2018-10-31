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
    [Route("api/categories")]
    [ApiController]
    [Authorize(Policy = "ApiUser")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            CategoryDTO categoryDTO = _mapper.Map<CategoryDTO>(model);
            await _categoryService.Create(categoryDTO);
            return Ok();
        }
    }
}