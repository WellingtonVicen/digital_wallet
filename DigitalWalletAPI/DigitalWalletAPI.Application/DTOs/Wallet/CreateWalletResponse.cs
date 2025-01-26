namespace DigitalWalletAPI.Application.DTOs.Wallet
{
    public class CreateWalletResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
