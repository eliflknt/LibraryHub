namespace LibraryHub.WebApi.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string ISBN { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public int PublicationYear { get; set; }

        public int StockQuantity { get; set; }

        public int RaftakiAdet { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }
    }
}