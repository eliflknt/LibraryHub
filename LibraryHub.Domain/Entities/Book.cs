using LibraryHub.Domain.Common;

namespace LibraryHub.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int PublishYear { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}