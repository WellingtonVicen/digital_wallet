using System.Transactions;

namespace DigitalWalletAPI.Application.Interfaces.Repositories
{
    namespace Application.Interfaces.Repositories
    {
        public interface ITransactionRepository
        {
            void Add(Transaction transaction);
            List<Transaction> GetTransactionsByUserId(int userId, DateTime? startDate = null, DateTime? endDate = null);
        }
    }
}
