namespace DigitalWalletAPI.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName, object resourceId)
            : base($"{resourceName} with ID '{resourceId}' was not found.")
        {
        }
    }
}
