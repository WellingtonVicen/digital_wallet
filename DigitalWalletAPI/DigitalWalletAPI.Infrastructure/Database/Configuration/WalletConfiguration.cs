using DigitalWalletAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigitalWalletAPI.Infrastructure.Database.Configuration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallets");

            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();
            builder.Property(w => w.Balance).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(w => w.User)
                   .WithMany(u => u.Wallets)
                   .HasForeignKey(w => w.UserId);
        }
    }
}
