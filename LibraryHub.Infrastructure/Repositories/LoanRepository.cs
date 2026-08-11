using LibraryHub.Application.Interfaces;
using LibraryHub.Domain.Entities;
using LibraryHub.Infrastructure.Persistence;

namespace LibraryHub.Infrastructure.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context)
            : base(context)
        {
        }
    }
}