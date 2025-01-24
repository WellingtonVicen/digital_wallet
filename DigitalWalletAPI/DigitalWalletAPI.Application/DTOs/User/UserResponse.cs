using System.Net;

namespace DigitalWalletAPI.Application.DTOs.User
{
    public class UserResponse
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
