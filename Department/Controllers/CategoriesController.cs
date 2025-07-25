using Application.DTOs.CategoryDto;
using Application.Interfaces.CategoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadCategoryDto>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadCategoryDto>> GetByCategoryIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult<ReadCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var newCategory = await _categoryService.CreateCategoryAsync(createCategoryDto);

            if (newCategory == null)
            {
                return BadRequest("Saqlanmadi yangi category");
            }
            return Ok(newCategory);
        }
        [HttpPut]
        public async Task<ActionResult<ReadCategoryDto>> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _categoryService.GetCategoryByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound($"Category with id {id} not found.");
            }
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, updateCategoryDto);
            if (updatedCategory == null)
            {
                return BadRequest("Category update failed.");
            }
            if (updateCategoryDto == null)
            {
                return BadRequest("Update data cannot be null.");
            }

            return Ok(updatedCategory);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCategoryAsync(int id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Category with id {id} not found.");
            }
            return Ok(isDeleted);
        }
    }
}
