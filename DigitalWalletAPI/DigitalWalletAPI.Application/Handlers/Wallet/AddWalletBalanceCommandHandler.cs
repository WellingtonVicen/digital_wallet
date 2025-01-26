using DigitalWalletAPI.Application.Commands.Wallet;
using DigitalWalletAPI.Application.DTOs.Wallet;
using DigitalWalletAPI.Application.Interfaces;
using DigitalWalletAPI.Application.Interfaces.Repositories;
using MediatR;
using System.Transactions;

namespace DigitalWalletAPI.Application.Handlers.Wallet
{
    public class AddWalletBalanceCommandHandler : IRequestHandler<AddWalletBalanceCommand, AddWalletBalanceResponse>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddWalletBalanceCommandHandler(IWalletRepository walletRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AddWalletBalanceResponse> Handle(AddWalletBalanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                if (request.Amount <= 0)
                    throw new ArgumentException("The amount to add must be greater than zero.", nameof(request.Amount));


                using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                // Verifica se a carteira já existe para o usuário
                var wallet = await _walletRepository.GetWalletByUserIdAsync(request.UserId, cancellationToken);

                if (wallet is null)
                {
                    wallet = new Domain.Entities.Wallet(request.UserId, /*"BRL",*/ request.Amount);

                    await _walletRepository.AddAsync(wallet, cancellationToken);
                }
                else
                {
                    wallet.AddBalance(request.Amount);
                    _walletRepository.Update(wallet);
                }

                await _unitOfWork.CommitAsync(cancellationToken);
                transactionScope.Complete();

                return new AddWalletBalanceResponse
                {
                    WalletId = wallet.Id,
                    NewBalance = wallet.Balance
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
