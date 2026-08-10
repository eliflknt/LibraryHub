using LibraryHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryHub.Infrastructure.Configurations;

public class FineConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(f => f.IsPaid)
            .IsRequired();

        builder.HasOne(f => f.Loan)
            .WithOne(l => l.Fine)
            .HasForeignKey<Fine>(f => f.LoanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}