using LibraryHub.Domain.Common;

namespace LibraryHub.Domain.Entities;

public class Author : BaseEntity
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}