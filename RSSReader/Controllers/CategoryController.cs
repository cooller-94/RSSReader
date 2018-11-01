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
    [Route("api/categories")]
    [ApiController]
    [Authorize(Policy = "ApiUser")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ClaimsPrincipal _caller;

        public CategoryController(ICategoryService categoryService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _caller = httpContextAccessor.HttpContext.User;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetAllCategoriesForUser(_caller.Claims.FirstOrDefault(c => c.Type == "id").Value);

            return Ok(_mapper.Map<IEnumerable<CategoryModel>>(categories));
        }
    }
}