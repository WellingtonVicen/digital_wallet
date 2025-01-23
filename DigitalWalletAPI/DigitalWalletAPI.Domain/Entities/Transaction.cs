namespace DigitalWalletAPI.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; private set; }
        public int? FromWalletId { get; private set; }
        public int ToWalletId { get; private set; }
        public decimal Amount { get; private set; }
        public string Type { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Wallet FromWallet { get; private set; }
        public Wallet ToWallet { get; private set; }

        private Transaction() { } // Construtor para frameworks de persistência

        public Transaction(Wallet fromWallet, Wallet toWallet, decimal amount, string description = "")
        {
            if (fromWallet == null && toWallet == null)
                throw new InvalidOperationException("At least one wallet must be specified.");

            if (amount <= 0)
                throw new InvalidOperationException("Transaction amount must be positive.");

            if (fromWallet != null && !fromWallet.HasSufficientBalance(amount))
                throw new InvalidOperationException("Insufficient balance in the source wallet.");

            FromWalletId = fromWallet?.Id;
            ToWalletId = toWallet.Id;
            Amount = amount;
            Type = fromWallet == null ? "Credit" : "Transfer";
            Description = description;
            CreatedAt = DateTime.UtcNow;

            // Atualizar saldos das carteiras diretamente
            fromWallet?.SubtractBalance(amount);
            toWallet.AddBalance(amount);
        }
    }

}
