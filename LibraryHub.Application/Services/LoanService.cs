using LibraryHub.Application.DTOs;
using LibraryHub.Application.Interfaces;
using LibraryHub.Application.Results;
using LibraryHub.Domain.Entities;
using LibraryHub.Domain.Enums;

namespace LibraryHub.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IGenericRepository<Loan> _loanRepository;
        private readonly IGenericRepository<Member> _memberRepository;
        private readonly IGenericRepository<Book> _bookRepository;
        private readonly IGenericRepository<Fine> _fineRepository;

        public LoanService(
            IGenericRepository<Loan> loanRepository,
            IGenericRepository<Member> memberRepository,
            IGenericRepository<Book> bookRepository,
            IGenericRepository<Fine> fineRepository)
        {
            _loanRepository = loanRepository;
            _memberRepository = memberRepository;
            _bookRepository = bookRepository;
            _fineRepository = fineRepository;
        }

        public async Task<Result<LoanDto>> BorrowBookAsync(int memberId, int bookId)
        {
            var member = await _memberRepository.GetByIdAsync(memberId);

            if (member == null)
                return Result<LoanDto>.Failure("Üye bulunamadı.");

            if (!member.IsActive)
                return Result<LoanDto>.Failure("Üye aktif değil.");

            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
                return Result<LoanDto>.Failure("Kitap bulunamadı.");

            if (book.RaftakiAdet <= 0)
                return Result<LoanDto>.Failure("Kitap rafta bulunmuyor.");

            var memberLoans = await _loanRepository.FindAsync(
                l => l.MemberId == memberId);

            var activeLoans = memberLoans
                .Where(l => l.Status == LoanStatus.Active)
                .ToList();

            if (activeLoans.Count >= 3)
                return Result<LoanDto>.Failure(
                    "Üye aynı anda en fazla 3 kitap ödünç alabilir.");

            if (activeLoans.Any(l => l.BookId == bookId))
                return Result<LoanDto>.Failure(
                    "Üye bu kitabı zaten ödünç almış.");

            var loanIds = memberLoans.Select(l => l.Id).ToList();

            if (loanIds.Any())
            {
                var unpaidFines = await _fineRepository.FindAsync(
                    f => loanIds.Contains(f.LoanId) && !f.IsPaid);

                if (unpaidFines.Any())
                    return Result<LoanDto>.Failure(
                        "Ödenmemiş cezası bulunan üye yeni kitap alamaz.");
            }

            var today = DateTime.UtcNow.Date;

            var loan = new Loan
            {
                MemberId = memberId,
                BookId = bookId,
                LoanDate = today,
                DueDate = today.AddDays(14),
                Status = LoanStatus.Active
            };

            book.RaftakiAdet--;

            await _loanRepository.AddAsync(loan);
            _bookRepository.Update(book);

            await _loanRepository.SaveChangesAsync();

            var loanDto = new LoanDto
            {
                Id = loan.Id,
                MemberId = loan.MemberId,
                BookId = loan.BookId,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status,
                FineAmount = null
            };

            return Result<LoanDto>.Success(loanDto);
        }

        public async Task<Result<LoanDto>> ReturnBookAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);

            if (loan == null)
                return Result<LoanDto>.Failure("Ödünç kaydı bulunamadı.");

            if (loan.ReturnDate.HasValue)
                return Result<LoanDto>.Failure(
                    "Bu kitap daha önce iade edilmiş.");

            var book = await _bookRepository.GetByIdAsync(loan.BookId);

            if (book == null)
                return Result<LoanDto>.Failure("Kitap bulunamadı.");

            var returnDate = DateTime.UtcNow.Date;

            loan.ReturnDate = returnDate;
            loan.Status = LoanStatus.Returned;

            book.RaftakiAdet++;

            decimal? fineAmount = null;

            var lateDays = (returnDate - loan.DueDate.Date).Days;

            if (lateDays > 0)
            {
                fineAmount = lateDays * 2m;

                var fine = new Fine
                {
                    LoanId = loan.Id,
                    Amount = fineAmount.Value,
                    IsPaid = false
                };

                await _fineRepository.AddAsync(fine);
            }

            _loanRepository.Update(loan);
            _bookRepository.Update(book);

            await _loanRepository.SaveChangesAsync();

            var loanDto = new LoanDto
            {
                Id = loan.Id,
                MemberId = loan.MemberId,
                BookId = loan.BookId,
                LoanDate = loan.LoanDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                Status = loan.Status,
                FineAmount = fineAmount
            };

            return Result<LoanDto>.Success(loanDto);
        }
    }
}