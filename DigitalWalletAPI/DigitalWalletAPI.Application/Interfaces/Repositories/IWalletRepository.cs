using DigitalWalletAPI.Domain.Entities;

namespace DigitalWalletAPI.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Wallet GetWalletByUserId(int userId);
        void Add(Wallet wallet);
        void Update(Wallet wallet);
        bool WalletExists(int userId);
    }
}
