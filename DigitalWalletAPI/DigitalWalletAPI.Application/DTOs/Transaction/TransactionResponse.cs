namespace DigitalWalletAPI.Application.DTOs.Transaction
{
    public class TransactionResponse
    {
        public long Id { get; set; }
        public long? FromWalletId { get; set; }
        public long ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
