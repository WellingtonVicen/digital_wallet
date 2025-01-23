namespace DigitalWalletAPI.Domain.Exceptions
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(decimal attemptedAmount, decimal availableBalance)
            : base($"Insufficient balance. Attempted: {attemptedAmount}, Available: {availableBalance}.")
        {
        }
    }
}
