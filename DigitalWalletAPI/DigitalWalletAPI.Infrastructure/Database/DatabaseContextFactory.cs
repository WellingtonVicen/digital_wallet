using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigitalWalletAPI.Infrastructure.Database
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DigitalWalletContext>
    {
        public DigitalWalletContext CreateDbContext(string[] args)
        {
            var connectionString = "";
            var optionsBuilder = new DbContextOptionsBuilder<DigitalWalletContext>();
            optionsBuilder.UseNpgsql(connectionString); 

            return new DigitalWalletContext(optionsBuilder.Options);    
        
        }
    }
}
