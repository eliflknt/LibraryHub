using LibraryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryHub.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(b => b.ISBN)
            .IsUnique();

        builder.Property(b => b.PublishYear)
            .IsRequired();

        builder.Property(b => b.StokAdedi)
            .IsRequired();

        builder.Property(b => b.RaftakiAdet)
            .IsRequired();

        builder.ToTable("Books", table =>
        {
            table.HasCheckConstraint(
                "CK_Books_RaftakiAdet",
                "\"RaftakiAdet\" >= 0");
        });

        builder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.BookAuthors)
            .WithOne(ba => ba.Book)
            .HasForeignKey(ba => ba.BookId);

        builder.HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId);

        builder.HasData(
            new Book
            {
                Id = 1,
                Title = "İnce Memed",
                ISBN = "9789750807178",
                PublishYear = 1955,
                StokAdedi = 5,
                RaftakiAdet = 5,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 2,
                Title = "Kürk Mantolu Madonna",
                ISBN = "9789753638029",
                PublishYear = 1943,
                StokAdedi = 5,
                RaftakiAdet = 5,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 3,
                Title = "1984",
                ISBN = "9780451524935",
                PublishYear = 1949,
                StokAdedi = 5,
                RaftakiAdet = 5,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 4,
                Title = "Dönüşüm",
                ISBN = "9780007921955",
                PublishYear = 1915,
                StokAdedi = 4,
                RaftakiAdet = 4,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 5,
                Title = "Dünyanın Merkezine Yolculuk",
                ISBN = "9786053321234",
                PublishYear = 1864,
                StokAdedi = 4,
                RaftakiAdet = 4,
                CategoryId = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 6,
                Title = "Denizler Altında Yirmi Bin Fersah",
                ISBN = "9786053325678",
                PublishYear = 1870,
                StokAdedi = 4,
                RaftakiAdet = 4,
                CategoryId = 2,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 7,
                Title = "Satranç",
                ISBN = "9783596294308",
                PublishYear = 1942,
                StokAdedi = 3,
                RaftakiAdet = 3,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 8,
                Title = "Beyaz Diş",
                ISBN = "9780451526842",
                PublishYear = 1906,
                StokAdedi = 3,
                RaftakiAdet = 3,
                CategoryId = 1,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 9,
                Title = "Savaş ve Barış",
                ISBN = "9780199232765",
                PublishYear = 1869,
                StokAdedi = 3,
                RaftakiAdet = 3,
                CategoryId = 3,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Book
            {
                Id = 10,
                Title = "Nutuk",
                ISBN = "9789751003031",
                PublishYear = 1927,
                StokAdedi = 5,
                RaftakiAdet = 5,
                CategoryId = 3,
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}