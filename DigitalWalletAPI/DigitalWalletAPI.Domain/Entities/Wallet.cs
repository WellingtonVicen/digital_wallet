
namespace DigitalWalletAPI.Domain.Entities
{
    public class Wallet
    {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }

        public Wallet() { }

        public Wallet(long userId, decimal initialBalance = 0)
        {
            UserId = userId;
            Balance = initialBalance >= 0 ? initialBalance : throw new ArgumentException("Initial balance cannot be negative.");
        }

        public void AddBalance(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount to add must be positive.");

            Balance += amount;
        }

        public void SubtractBalance(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Amount to subtract must be positive.");

            if (Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            Balance -= amount;
        }

        public bool HasSufficientBalance(decimal amount)
        {
            return Balance >= amount;
        }
    }
}
