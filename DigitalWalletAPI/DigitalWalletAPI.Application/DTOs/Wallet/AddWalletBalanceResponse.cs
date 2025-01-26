namespace DigitalWalletAPI.Application.DTOs.Wallet
{
    public class AddWalletBalanceResponse
    {
        public long WalletId { get; set; }
        public decimal NewBalance { get; set; }
    }
}
