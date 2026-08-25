using LibraryHub.Application.DTOs.Reports;
using LibraryHub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryHub.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("most-borrowed-books")]
        public async Task<ActionResult<List<MostBorrowedBookDto>>> GetMostBorrowedBooks()
        {
            var result = await _reportService.GetMostBorrowedBooksAsync();

            return Ok(result);
        }

        [HttpGet("overdue-loans")]
        public async Task<ActionResult<List<OverdueLoanDto>>> GetOverdueLoans()
        {
            var result = await _reportService.GetOverdueLoansAsync();

            return Ok(result);
        }

        [HttpGet("monthly-loans")]
        public async Task<ActionResult<List<MonthlyLoanReportDto>>> GetMonthlyLoanCounts()
        {
            var result = await _reportService.GetMonthlyLoanCountsAsync();

            return Ok(result);
        }

        [HttpGet("unpaid-fines")]
        public async Task<ActionResult<UnpaidFineReportDto>> GetTotalUnpaidFines()
        {
            var result = await _reportService.GetTotalUnpaidFinesAsync();

            return Ok(result);
        }
    }
}