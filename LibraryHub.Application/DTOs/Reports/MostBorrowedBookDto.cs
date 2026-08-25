namespace LibraryHub.Application.DTOs.Reports;

public class MostBorrowedBookDto
{
    public int BookId { get; set; }

    public string Title { get; set; } = string.Empty;

    public int BorrowCount { get; set; }
}