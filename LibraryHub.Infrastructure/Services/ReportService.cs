using LibraryHub.Application.DTOs.Reports;
using LibraryHub.Application.Interfaces;
using LibraryHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryHub.Infrastructure.Services;

public class ReportService : IReportService
{
    private readonly LibraryDbContext _context;

    public ReportService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<MostBorrowedBookDto>> GetMostBorrowedBooksAsync()
    {
        var loans = await _context.Loans
            .AsNoTracking()
            .Include(l => l.Book)
            .ToListAsync();

        return loans
            .GroupBy(l => new
            {
                l.BookId,
                l.Book.Title
            })
            .Select(g => new MostBorrowedBookDto
            {
                BookId = g.Key.BookId,
                Title = g.Key.Title,
                BorrowCount = g.Count()
            })
            .OrderByDescending(x => x.BorrowCount)
            .Take(5)
            .ToList();
    }

    public async Task<List<OverdueLoanDto>> GetOverdueLoansAsync()
    {
        var today = DateTime.UtcNow.Date;

        return await _context.Loans
            .AsNoTracking()
            .Where(l =>
                l.ReturnDate == null &&
                l.DueDate.Date < today)
            .Select(l => new OverdueLoanDto
            {
                LoanId = l.Id,
                BookTitle = l.Book.Title,
                MemberName = l.Member.FullName,
                DueDate = l.DueDate,
                OverdueDays = (today - l.DueDate.Date).Days
            })
            .OrderBy(x => x.DueDate)
            .ToListAsync();
    }

    public async Task<List<MonthlyLoanReportDto>> GetMonthlyLoanCountsAsync()
    {
        return await _context.Loans
            .AsNoTracking()
            .GroupBy(l => new
            {
                l.LoanDate.Year,
                l.LoanDate.Month
            })
            .Select(g => new MonthlyLoanReportDto
            {
                Year = g.Key.Year,
                Month = g.Key.Month,
                LoanCount = g.Count()
            })
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .ToListAsync();
    }

    public async Task<UnpaidFineReportDto> GetTotalUnpaidFinesAsync()
    {
        var totalUnpaidAmount = await _context.Fines
            .AsNoTracking()
            .Where(f => !f.IsPaid)
            .SumAsync(f => f.Amount);

        return new UnpaidFineReportDto
        {
            TotalUnpaidAmount = totalUnpaidAmount
        };
    }
}