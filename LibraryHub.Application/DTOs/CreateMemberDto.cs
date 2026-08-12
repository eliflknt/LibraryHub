namespace LibraryHub.Application.DTOs
{
    public class CreateMemberDto
    {
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime UyelikTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}