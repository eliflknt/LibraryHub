namespace LibraryHub.WebApi.ViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }

        public string AdSoyad { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Telefon { get; set; } = string.Empty;

        public DateTime UyelikTarihi { get; set; }

        public bool AktifMi { get; set; }
    }
}