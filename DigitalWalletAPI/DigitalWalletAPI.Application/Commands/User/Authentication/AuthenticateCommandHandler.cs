using DigitalWalletAPI.Application.DTOs.User.Authentication;
using DigitalWalletAPI.Application.Interfaces.Hasher;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Application.Interfaces.Token;
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
                return new AuthenticationResponse();
            }

            // Gera o token JWT
            return new AuthenticationResponse
            {
                Data = new
                {
                    Token = GenerateToken(),
                    TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]!))
                }
            };
        }


        //summary>
        /// Gera um token JWT para o usuário autenticado.
        /// </summary>
        private string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, _configuration["Jwt:Login"]!),
                    new Claim(ClaimTypes.Role, "User")
                ]),
                Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]!)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
