
namespace DigitalWalletAPI.Domain.Entities
{
    public class Wallet
    {
        public int Id { get; private set; }
        public int UserId { get; private set; }
        public decimal Balance { get; private set; }
        public string Currency { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; }

        private Wallet() { } // Construtor para frameworks de persistência

        public Wallet(int userId, string currency, decimal initialBalance = 0)
        {
            UserId = userId;
            Currency = !string.IsNullOrWhiteSpace(currency) ? currency : throw new ArgumentException("Currency cannot be empty.");
            Balance = initialBalance >= 0 ? initialBalance : throw new ArgumentException("Initial balance cannot be negative.");
            CreatedAt = DateTime.UtcNow;
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
