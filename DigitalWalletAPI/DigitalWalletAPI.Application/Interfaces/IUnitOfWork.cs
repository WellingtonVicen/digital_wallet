namespace DigitalWalletAPI.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
    }
}
