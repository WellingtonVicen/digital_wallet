using DigitalWalletAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWalletAPI.Infrastructure.Database.Configuration
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(t => t.FromWallet)
                      .WithMany()
                      .HasForeignKey(t => t.FromWalletId)
                      .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.ToWallet)
                      .WithMany()
                      .HasForeignKey(t => t.ToWalletId)
                      .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
