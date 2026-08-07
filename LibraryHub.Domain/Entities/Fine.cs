using LibraryHub.Domain.Common;

namespace LibraryHub.Domain.Entities;

public class Fine : BaseEntity
{
    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }

    public int LoanId { get; set; }

    public Loan Loan { get; set; } = null!;
}