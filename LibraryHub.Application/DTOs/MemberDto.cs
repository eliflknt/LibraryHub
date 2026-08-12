namespace LibraryHub.Application.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime UyelikTarihi { get; set; }
        public bool AktifMi { get; set; }
    }
}