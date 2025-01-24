using DigitalWalletAPI.Application.DTOs.User.Authentication;
using DigitalWalletAPI.Application.Interfaces.Hasher;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalWalletAPI.Application.Commands.User.Authentication
{
    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticationResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ILogger<AuthenticateCommandHandler> _logger;
        private readonly IConfiguration _configuration;

        public AuthenticateCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
                                          ILogger<AuthenticateCommandHandler> logger, IConfiguration configuration)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }


        public async Task<AuthenticationResponse> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null || !_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
            {
                return new AuthenticationResponse { HttpStatusCode = System.Net.HttpStatusCode.Unauthorized };
            }

            // Gera o token JWT
            var token = GenerateJwtToken(user.Id, user.Email);

            return new AuthenticationResponse
            {
                HttpStatusCode = System.Net.HttpStatusCode.OK,
                UserResponse = new DTOs.User.UserResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name
                },
                Token = token,
            };
        }


        //summary>
        /// Gera um token JWT para o usuário autenticado.
        /// </summary>
        private string GenerateJwtToken(long userId, string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Recupera a chave com 256 bits a partir da configuração
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
