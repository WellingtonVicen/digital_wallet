using DigitalWalletAPI.Application.Interfaces.Repositories;
using DigitalWalletAPI.Domain.Entities;

namespace DigitalWalletAPI.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        public void Add(Wallet wallet)
        {
            throw new NotImplementedException();
        }

        public Wallet GetWalletByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Wallet wallet)
        {
            throw new NotImplementedException();
        }

        public bool WalletExists(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
