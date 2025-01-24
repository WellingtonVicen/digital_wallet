using DigitalWalletAPI.Application.DTOs.User;
using DigitalWalletAPI.Application.Interfaces.Hasher;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalWalletAPI.Application.Commands.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Iniciando criação de usuário para {Email}.", request.Email);

            // Validando a existência do usuário
            var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (existingUser != null)
            {
                _logger.LogWarning("Tentativa de criar usuário com email já existente: {Email}", request.Email);
                throw new UserAlreadyExistsException("Já existe um usuário com este email.");
            }

            // Criando a entidade User
            var newUser = new DigitalWalletAPI.Domain.Entities.User(request.Name, request.Email, _passwordHasher.HashPassword(request.Password));

            try
            {
                // Salvando no repositório
                await _userRepository.AddAsync(newUser, cancellationToken);

                // Logando sucesso
                _logger.LogInformation("Usuário criado com sucesso: {Email}", request.Email);

                // Retornando a resposta
                return new UserResponse
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    Email = newUser.Email
                };
            }
            catch (Exception ex)
            {
                // Em caso de erro, logamos a falha
                _logger.LogError(ex, "Erro ao criar usuário para {Email}.", request.Email);
                throw new ApplicationException("Erro ao criar usuário. Tente novamente mais tarde.");
            }
        }
    }
}
