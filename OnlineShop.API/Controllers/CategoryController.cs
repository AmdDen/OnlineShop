using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Bll.Interfaces;
using OnlineShop.Common.Dtos.Category;
using System.Threading.Tasks;

namespace OnlineShop.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class CategoryController : AppBaseController
    {
        private readonly ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }

        // api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var categoryDto = await _services.GetCategoryById(id);
            return Ok(categoryDto);
        }


        // api/category/
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _services.GetAllCategories();

            return Ok(categories);
        }


        // api/category/
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryDto = await _services.CreateCategory(createCategoryDto);

            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }


        // api/update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _services.UpdateCategory(id, dto);
            return Ok();
        }
    }
}
