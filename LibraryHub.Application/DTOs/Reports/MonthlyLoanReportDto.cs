namespace LibraryHub.Application.DTOs.Reports;

public class MonthlyLoanReportDto
{
    public int Year { get; set; }

    public int Month { get; set; }

    public int LoanCount { get; set; }
}