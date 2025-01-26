using DigitalWalletAPI.Application.Interfaces.Repositories;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Infrastructure.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly DigitalWalletContext _digitalWalletContext;

        public WalletRepository(DigitalWalletContext digitalWalletContext)
        {
            _digitalWalletContext = digitalWalletContext ?? throw new ArgumentNullException(nameof(digitalWalletContext));
        }

        public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken)
        {
            await _digitalWalletContext.Wallets.AddAsync(wallet, cancellationToken);
        }

        public async Task<decimal> GetBalanceWalletByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            // Busca o saldo da carteira pelo userId
            var wallet = await _digitalWalletContext.Wallets.Where(w => w.UserId == userId)
                                                            .Select(w => w.Balance)
                                                            .FirstOrDefaultAsync(cancellationToken);

            return wallet;
        }

        public async Task<Wallet?> GetWalletByUserIdAsync(long userId, CancellationToken cancellationToken)
        {
            return await _digitalWalletContext.Wallets.AsNoTracking()
                            .FirstOrDefaultAsync(wallet => wallet.UserId == userId, cancellationToken);
        }

        public async Task<Wallet?> GetWalletByWalletIdAsync(long walletId, CancellationToken cancellationToken)
        {
           return await _digitalWalletContext.Wallets.FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);    
        }

        public void Update(Wallet wallet)
        {
           _digitalWalletContext.Wallets.Update(wallet); 
        }

        public bool WalletExists(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
