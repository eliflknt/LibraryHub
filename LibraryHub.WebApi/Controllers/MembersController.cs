using LibraryHub.Application.DTOs;
using LibraryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _memberService.GetAllAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _memberService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMemberDto dto)
        {
            var result = await _memberService.CreateAsync(dto);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMemberDto dto)
        {
            var result = await _memberService.UpdateAsync(id, dto);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
    }
}