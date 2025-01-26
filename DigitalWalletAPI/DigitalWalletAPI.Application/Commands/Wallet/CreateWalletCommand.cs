using DigitalWalletAPI.Application.DTOs.Wallet;
using MediatR;

namespace DigitalWalletAPI.Application.Commands.Wallet
{
    public class CreateWalletCommand : IRequest<CreateWalletResponse>
    {
        public string Name { get; set; } = string.Empty;
        public long UserId { get; set; }
        public decimal InitialBalance { get; set; }
    }
}
