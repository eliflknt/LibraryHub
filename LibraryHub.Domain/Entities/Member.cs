using LibraryHub.Domain.Common;

namespace LibraryHub.Domain.Entities;

public class Member : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}