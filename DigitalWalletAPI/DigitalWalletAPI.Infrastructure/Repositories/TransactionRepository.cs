using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using System.Transactions;

namespace DigitalWalletAPI.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public void Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public List<Transaction> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null)
        {
            throw new NotImplementedException();
        }
    }
}
