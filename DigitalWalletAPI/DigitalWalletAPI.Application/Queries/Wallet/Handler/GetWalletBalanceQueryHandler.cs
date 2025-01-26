using DigitalWalletAPI.Application.DTOs.Wallet;
using DigitalWalletAPI.Application.Interfaces.Repositories;
using MediatR;

namespace DigitalWalletAPI.Application.Queries.Wallet.Handler
{
    public class GetWalletBalanceQueryHandler : IRequestHandler<GetWalletBalanceQuery, GetWalletBalanceResponse>
    {
        private readonly IWalletRepository _walletRepository;
        public GetWalletBalanceQueryHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<GetWalletBalanceResponse> Handle(GetWalletBalanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    throw new ArgumentNullException(nameof(request), "The query request cannot be null.");

                // Busca o saldo da carteira baseado no UserId
                var balance = await _walletRepository.GetBalanceWalletByUserIdAsync(request.UserId, cancellationToken);

                // Retorna a resposta
                return new GetWalletBalanceResponse
                {
                    Balance = balance
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
