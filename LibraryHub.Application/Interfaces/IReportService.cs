using LibraryHub.Application.DTOs.Reports;

namespace LibraryHub.Application.Interfaces;

public interface IReportService
{
    Task<List<MostBorrowedBookDto>> GetMostBorrowedBooksAsync();

    Task<List<OverdueLoanDto>> GetOverdueLoansAsync();

    Task<List<MonthlyLoanReportDto>> GetMonthlyLoanCountsAsync();

    Task<UnpaidFineReportDto> GetTotalUnpaidFinesAsync();
}