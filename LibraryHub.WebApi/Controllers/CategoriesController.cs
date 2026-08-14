using LibraryHub.Application.DTOs;
using LibraryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
                return NotFound("Kategori bulunamadı.");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var category = await _categoryService.CreateAsync(dto);

            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCategoryDto dto)
        {
            var category = await _categoryService.UpdateAsync(id, dto);

            if (category == null)
                return NotFound("Kategori bulunamadı.");

            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result)
                return NotFound("Kategori bulunamadı.");

            return Ok(result);
        }
    }
}