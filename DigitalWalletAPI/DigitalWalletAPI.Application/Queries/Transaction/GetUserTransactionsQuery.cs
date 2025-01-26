using DigitalWalletAPI.Application.DTOs.Transaction;
using MediatR;

namespace DigitalWalletAPI.Application.Queries.Transaction
{
    public class GetUserTransactionsQuery : IRequest<List<TransactionResponse>>
    {
        public long UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
