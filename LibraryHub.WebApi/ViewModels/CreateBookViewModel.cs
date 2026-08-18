using System.ComponentModel.DataAnnotations;

namespace LibraryHub.WebApi.ViewModels
{
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "ISBN alanı zorunludur.")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kitap başlığı zorunludur.")]
        public string Title { get; set; } = string.Empty;

        [Range(0, 2100, ErrorMessage = "Yayın yılı geçerli değil.")]
        public int PublicationYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok adedi negatif olamaz.")]
        public int StockQuantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Raftaki adet negatif olamaz.")]
        public int RaftakiAdet { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public int CategoryId { get; set; }
    }
}