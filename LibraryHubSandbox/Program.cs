using LibraryHubSandbox;
using System.Linq;

List<Book> books = new List<Book>
{
    new Book
    {
        Title = "Suç ve Ceza",
        Author = "Dostoyevski",
        PublishYear = 1866,
        PageCount = 687,
        Category = "Roman"
    },

    new Book
    {
        Title = "1984",
        Author = "George Orwell",
        PublishYear = 1949,
        PageCount = 328,
        Category = "Distopya"
    },

    new Book
    {
        Title = "Clean Code",
        Author = "Robert C. Martin",
        PublishYear = 2020,
        PageCount = 464,
        Category = "Yazılım"
    },

    new Book
    {
        Title = "Atomik Alışkanlıklar",
        Author = "James Clear",
        PublishYear = 2021,
        PageCount = 320,
        Category = "Kişisel Gelişim"
    },

    new Book
    {
        Title = "İnsan Ne ile Yaşar",
        Author = "Tolstoy",
        PublishYear = 2022,
        PageCount = 120,
        Category = "Roman"
    }
};

// 2020 sonrası kitaplar
var booksAfter2020 = books.Where(b => b.PublishYear > 2020);

Console.WriteLine("=== 2020 Sonrası Kitaplar ===");

foreach (var book in booksAfter2020)
{
    Console.WriteLine(book.Title);
}

// Kategoriye göre gruplama
var groups = books.GroupBy(b => b.Category);

Console.WriteLine("\n=== Kategoriler ===");

foreach (var group in groups)
{
    Console.WriteLine($"\nKategori: {group.Key}");

    foreach (var book in group)
    {
        Console.WriteLine(book.Title);
    }
}

// En çok sayfalı kitap
var maxPageBook = books
    .OrderByDescending(b => b.PageCount)
    .First();

Console.WriteLine($"\nEn Çok Sayfalı Kitap: {maxPageBook.Title}");

// Any()
bool hasSoftwareBook = books.Any(b => b.Category == "Yazılım");

Console.WriteLine($"\nYazılım kitabı var mı? {hasSoftwareBook}");

// Select()
var titles = books.Select(b => b.Title);

Console.WriteLine("\n=== Kitap İsimleri ===");

foreach (var title in titles)
{
    Console.WriteLine(title);
}

// FirstOrDefault()
var firstRoman = books.FirstOrDefault(b => b.Category == "Roman");

Console.WriteLine($"\nİlk Roman: {firstRoman?.Title}");

// Async örneği
await GetBooksAsync();

static async Task<List<Book>> GetBooksAsync()
{
    await Task.Delay(2000);

    Console.WriteLine("\nVeriler başarıyla getirildi.");

    return new List<Book>();
}