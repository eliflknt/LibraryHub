namespace LibraryHub.Application.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Baslik { get; set; }
        public int YayinYili { get; set; }
        public int StokAdedi { get; set; }
        public int RaftakiAdet { get; set; }
        public int CategoryId { get; set; }
    }
}