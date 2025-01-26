using DigitalWalletAPI.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DigitalWalletAPI.Infrastructure.Database.UnitOfWork
{
    public class UnitOfWork(DigitalWalletContext context) : IUnitOfWork
    {
        private readonly DigitalWalletContext _context = context;

        /// <summary>
        /// Abre a Transaction com o banco de dados.
        /// </summary>
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Confirma todas as alterações no banco de dados.
        /// </summary>
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
