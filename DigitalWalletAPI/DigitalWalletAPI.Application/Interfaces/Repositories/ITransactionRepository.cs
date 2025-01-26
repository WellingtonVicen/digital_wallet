using System.Transactions;

namespace DigitalWalletAPI.Application.Interfaces.Repositories
{
    namespace Application.Interfaces.Repositories
    {
        public interface ITransactionRepository
        {
            Task AddAsync(Domain.Entities.Transaction transaction, CancellationToken cancellationToken);
            Task<List<Domain.Entities.Transaction>> GetTransactionsByUserIdAsync(long userId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default);
        }
    }
}
