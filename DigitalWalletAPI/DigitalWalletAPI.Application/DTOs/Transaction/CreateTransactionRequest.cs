namespace DigitalWalletAPI.Application.DTOs.Transaction
{
    namespace DigitalWalletAPI.API.DTOs.Transaction
    {
        public class CreateTransactionRequest
        {
            public long FromWalletId { get; set; }
            public long ToWalletId { get; set; }
            public decimal Amount { get; set; }
            public string? Description { get; set; }
        }
    }
}
