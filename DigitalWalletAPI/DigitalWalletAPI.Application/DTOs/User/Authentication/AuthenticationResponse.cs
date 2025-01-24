using System.Net;

namespace DigitalWalletAPI.Application.DTOs.User.Authentication
{
    public class AuthenticationResponse
    {
        public UserResponse? UserResponse { get; set; }
        public string? Token { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
