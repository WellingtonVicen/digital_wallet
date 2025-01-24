namespace DigitalWalletAPI.Application.Interfaces
{
    public interface ITransactionService
    {
        void CreateTransaction(int fromUserId, int toUserId, decimal amount, string description);
        //List<TransactionDto> GetUserTransactions(int userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
