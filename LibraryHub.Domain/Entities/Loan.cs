using LibraryHub.Domain.Common;
using LibraryHub.Domain.Enums;
using static System.Net.WebRequestMethods;

namespace LibraryHub.Domain.Entities;

public class Loan : BaseEntity
{
    public int MemberId { get; set; }

    public Member Member { get; set; } = null!;

    public int BookId { get; set; }

    public Book Book { get; set; } = null!;

    public DateTime LoanDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public LoanStatus Status { get; set; }

    public Fine? Fine { get; set; }
}