using LibraryHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestRepositoryController : ControllerBase
    {
        private readonly ILoanRepository _loanRepository;

        public TestRepositoryController(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoans()
        {
            var loans = await _loanRepository.GetAllAsync();

            return Ok(loans);
        }
    }
}