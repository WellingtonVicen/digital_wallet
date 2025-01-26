namespace DigitalWalletAPI.Application.DTOs.Wallet
{
    public class AddWalletBalanceRequest
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
