namespace DigitalWalletAPI.Application.Interfaces
{
    public interface IWalletService
    {
        void CreateWallet(int userId, string currency);
        decimal GetBalance(int userId);
        void AddBalance(int userId, decimal amount);
        void SubtractBalance(int userId, decimal amount);
        void TransferFunds(int fromUserId, int toUserId, decimal amount, string description = "");
        //List<TransactionDto> GetTransactions(int userId, DateTime? startDate = null, DateTime? endDate = null);
    }
}
