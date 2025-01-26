using DigitalWalletAPI.Application.DTOs.Transaction;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Application.Queries.Transaction;
using MediatR;

namespace DigitalWalletAPI.Application.Handlers.Transaction
{
    public class GetUserTransactionsQueryHandler : IRequestHandler<GetUserTransactionsQuery, List<TransactionResponse>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetUserTransactionsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<List<TransactionResponse>> Handle(GetUserTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await  _transactionRepository.GetTransactionsByUserIdAsync(
                request.UserId,
                request.StartDate,
                request.EndDate,
                cancellationToken);

            return transactions.Select(t => new TransactionResponse
            {
                Id = t.Id,
                Amount = t.Amount,
                CreatedAt = t.CreatedAt,
                Description = t.Description
            }).ToList();
        }
    }
}
