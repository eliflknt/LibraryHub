using LibraryHub.Domain.Enums;

namespace LibraryHub.Application.DTOs
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public LoanStatus Status { get; set; }
        public decimal? FineAmount { get; set; }
    }
}