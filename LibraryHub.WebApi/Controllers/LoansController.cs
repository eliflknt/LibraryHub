using LibraryHub.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook(int memberId, int bookId)
        {
            var result = await _loanService.BorrowBookAsync(memberId, bookId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }

        [HttpPut("{loanId}/return")]
        public async Task<IActionResult> ReturnBook(int loanId)
        {
            var result = await _loanService.ReturnBookAsync(loanId);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return Ok(result.Data);
        }
    }
}