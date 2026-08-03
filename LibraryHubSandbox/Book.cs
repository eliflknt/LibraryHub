namespace LibraryHubSandbox;

public class Book : IBorrowable
{
    public string Title { get; set; }

    public string Author { get; set; }

    public int PublishYear { get; set; }

    public int PageCount { get; set; }

    public string Category { get; set; }
    public void Borrow()
    {
        Console.WriteLine($"{Title} ödünç verildi.");
    }

    public void Return()
    {
        Console.WriteLine($"{Title} geri alındı.");
    }
}