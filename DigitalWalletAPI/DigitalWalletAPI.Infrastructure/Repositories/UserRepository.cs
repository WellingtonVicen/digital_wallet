using DigitalWalletAPI.Application.Interfaces;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Domain.Entities;
using DigitalWalletAPI.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace DigitalWalletAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DigitalWalletContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(DigitalWalletContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string? email, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User> GetById(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<bool> UserExists(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
