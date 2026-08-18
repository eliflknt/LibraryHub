using System.ComponentModel.DataAnnotations;

namespace LibraryHub.WebApi.ViewModels
{
    public class BorrowBookViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Üye seçimi zorunludur.")]
        public int MemberId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Kitap seçimi zorunludur.")]
        public int BookId { get; set; }
    }
}