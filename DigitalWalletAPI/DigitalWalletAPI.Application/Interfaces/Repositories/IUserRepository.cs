using DigitalWalletAPI.Domain.Entities;

namespace DigitalWalletAPI.Application.Interfaces.Repositories
{
    namespace Application.Interfaces.Repositories
    {
        public interface IUserRepository
        {
            Task<User> GetById(int userId);
            Task<User?> GetByEmailAsync(string? email, CancellationToken cancellationToken);
            Task AddAsync(User user, CancellationToken cancellationToken);
            Task<bool> UserExists(int userId);
        }
    }
}
