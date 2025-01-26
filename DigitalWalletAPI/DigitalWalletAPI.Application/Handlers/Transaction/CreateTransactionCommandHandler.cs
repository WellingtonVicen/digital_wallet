using DigitalWalletAPI.Application.Commands.Transaction;
using DigitalWalletAPI.Application.DTOs.Transaction;
using DigitalWalletAPI.Application.Interfaces;
using DigitalWalletAPI.Application.Interfaces.Repositories;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Domain.Exceptions;
using MediatR;

namespace DigitalWalletAPI.Application.Handlers.Transaction
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CreateTransactionCommandHandler(IUnitOfWork unitOfWork, IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        public async Task<TransactionResponse> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                throw new InvalidOperationException("Amount must be greater than zero.");

            using (var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var fromWallet = await _walletRepository.GetWalletByWalletIdAsync(request.FromWalletId, cancellationToken);
                    var toWallet = await _walletRepository.GetWalletByWalletIdAsync(request.ToWalletId, cancellationToken);

                    if (fromWallet is null || toWallet is null)
                        throw new NotFoundException("One or both wallets not found.", request);

                    if (!fromWallet.HasSufficientBalance(request.Amount))
                        throw new InsufficientBalanceException(request.Amount, fromWallet.Balance);

                    var transactionEntity = new Domain.Entities.Transaction(fromWallet, toWallet, request.Amount, request.Description);
                    await _transactionRepository.AddAsync(transactionEntity, cancellationToken);
                    
                    await transaction.CommitAsync(cancellationToken);

                    return new TransactionResponse
                    {
                        Id = transactionEntity.Id,
                        FromWalletId = transactionEntity.FromWalletId,
                        ToWalletId = transactionEntity.ToWalletId,
                        Amount = transactionEntity.Amount,
                        Description = transactionEntity.Description,
                        CreatedAt = transactionEntity.CreatedAt
                    };
                }
                catch (Exception)
                {
                    // Em caso de falha, realizar rollback
                    await transaction.RollbackAsync(cancellationToken);
                    throw;
                }
            }
        }
    }
}
