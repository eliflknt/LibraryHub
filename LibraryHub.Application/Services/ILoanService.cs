using LibraryHub.Application.DTOs;
using LibraryHub.Application.Results;

namespace LibraryHub.Application.Services
{
    public interface ILoanService
    {
        Task<Result<LoanDto>> BorrowBookAsync(int memberId, int bookId);
        Task<Result<LoanDto>> ReturnBookAsync(int loanId);
    }
}