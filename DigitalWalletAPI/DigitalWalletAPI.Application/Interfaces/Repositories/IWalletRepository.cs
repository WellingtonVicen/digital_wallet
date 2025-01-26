using DigitalWalletAPI.Domain.Entities;

namespace DigitalWalletAPI.Application.Interfaces.Repositories
{
    public interface IWalletRepository
    {
        Task<Wallet?> GetWalletByUserIdAsync(long userId, CancellationToken cancellationToken);
        Task<Wallet?> GetWalletByWalletIdAsync(long walletId, CancellationToken cancellationToken);
        Task<decimal> GetBalanceWalletByUserIdAsync(long userId, CancellationToken cancellationToken);
        Task AddAsync(Wallet wallet, CancellationToken cancellationToken);
        void Update(Wallet wallet);
        bool WalletExists(int userId);
    }
}
