using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Infrastructure.Database.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Infrastructure.Database
{
    public class DigitalWalletContext(DbContextOptions<DigitalWalletContext> options) : DbContext(options)
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new WalletConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());    
        }
    }
}
