using System.ComponentModel.DataAnnotations;

namespace DigitalWalletAPI.Application.DTOs.User.Authentication
{
    public class AuthenticateUserRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}

