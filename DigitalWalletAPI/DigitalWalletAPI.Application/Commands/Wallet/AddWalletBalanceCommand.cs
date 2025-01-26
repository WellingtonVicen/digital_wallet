using DigitalWalletAPI.Application.DTOs.Wallet;
using MediatR;

namespace DigitalWalletAPI.Application.Commands.Wallet
{
    public class AddWalletBalanceCommand : IRequest<AddWalletBalanceResponse>
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
