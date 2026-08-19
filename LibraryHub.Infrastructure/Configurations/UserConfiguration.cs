using LibraryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryHub.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.Role)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(
            new User
            {
                Id = 1001,
                Email = "admin@libraryhub.com",
                PasswordHash = "100000.MiuBPTHhq81y1s18F7hUKA==.aUyiwiIvJsPfqPeEVk6csHU00xsrRh95QALye1S1ot0=",
                Role = "Admin",
                IsActive = true,
                CreatedAt = new DateTime(2026, 8, 19, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = 1002,
                Email = "librarian@libraryhub.com",
                PasswordHash = "100000.FBn+sF9YO1ffISS9eN5NOg==.AlrLZ0auUnmkkHVpVszLHzXwp9Gi1seCQGMVaHBZCBI=",
                Role = "Librarian",
                IsActive = true,
                CreatedAt = new DateTime(2026, 8, 19, 0, 0, 0, DateTimeKind.Utc)
            },
            new User
            {
                Id = 1003,
                Email = "member@libraryhub.com",
                PasswordHash = "100000.IdYbp8ZEpCo9h36CKJdFPQ==.Nyvb2WCHQvPSJr4nW9yFTSloWiKdduA1guPLVgYHzv8=",
                Role = "Member",
                IsActive = true,
                CreatedAt = new DateTime(2026, 8, 19, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}