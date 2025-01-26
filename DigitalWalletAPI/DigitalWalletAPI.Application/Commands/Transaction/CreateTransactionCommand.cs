using DigitalWalletAPI.Application.DTOs.Transaction;
using MediatR;

namespace DigitalWalletAPI.Application.Commands.Transaction
{
    public class CreateTransactionCommand : IRequest<TransactionResponse>
    {
        public long FromWalletId { get; set; }
        public long ToWalletId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
