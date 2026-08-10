using LibraryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryHub.Infrastructure.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.FullName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(m => m.Phone)
            .HasMaxLength(20);

        builder.HasMany(m => m.Loans)
            .WithOne(l => l.Member)
            .HasForeignKey(l => l.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Member
            {
                Id = 1,
                FullName = "Ahmet Yılmaz",
                Email = "ahmet.yilmaz@example.com",
                Phone = "05551112233",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 2,
                FullName = "Ayşe Demir",
                Email = "ayse.demir@example.com",
                Phone = "05552223344",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Member
            {
                Id = 3,
                FullName = "Mehmet Kaya",
                Email = "mehmet.kaya@example.com",
                Phone = "05553334455",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}