using LibraryHub.Application.DTOs;
using LibraryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bookService.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bookService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookDto dto)
        {
            var result = await _bookService.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.Id },
                result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBookDto dto)
        {
            var result = await _bookService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Data);
        }
    }
}