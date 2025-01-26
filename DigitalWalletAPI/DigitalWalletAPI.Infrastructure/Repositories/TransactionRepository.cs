using DigitalWalletAPI.Application.Interfaces;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DigitalWalletContext _digitalWalletContext;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionRepository(DigitalWalletContext digitalWalletContext, IUnitOfWork unitOfWork)
        {
            _digitalWalletContext = digitalWalletContext ?? throw new ArgumentNullException(nameof(digitalWalletContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AddAsync(Domain.Entities.Transaction transaction, CancellationToken cancellationToken)
        {
            await _digitalWalletContext.Transactions.AddAsync(transaction, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Transaction>> GetTransactionsByUserIdAsync(long userId, DateTime? startDate = null, DateTime? endDate = null, CancellationToken cancellationToken = default)
        {
            var query = _digitalWalletContext.Transactions.AsQueryable();

            query = query.Where(t => t.FromWallet.UserId == userId);

            if (startDate.HasValue)
                query = query.Where(t => t.CreatedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(t => t.CreatedAt <= endDate.Value);

            return await query.ToListAsync(cancellationToken);
        }
    }
}
