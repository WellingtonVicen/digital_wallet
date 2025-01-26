using DigitalWalletAPI.Application.DTOs.Wallet;
using MediatR;

namespace DigitalWalletAPI.Application.Queries.Wallet
{
    public class GetWalletBalanceQuery : IRequest<GetWalletBalanceResponse>
    {
        public long WalletId { get; set; }
        public long UserId { get; set; }
    }
}
