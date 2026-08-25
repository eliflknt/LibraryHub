namespace LibraryHub.Application.DTOs.Reports;

public class OverdueLoanDto
{
    public int LoanId { get; set; }

    public string BookTitle { get; set; } = string.Empty;

    public string MemberName { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public int OverdueDays { get; set; }
}