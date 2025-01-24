using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalWalletAPI.Infrastructure.Mediator
{
    public class MediatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<MediatorPipelineBehavior<TRequest, TResponse>> _logger;

        public MediatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<MediatorPipelineBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Validação
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = _validators
                    .Select(v => v.Validate(context))
                    .Where(r => !r.IsValid)
                    .ToList();

                if (validationResults.Any())
                {
                    var failures = validationResults
                        .SelectMany(r => r.Errors)
                        .ToList();

                    // Extraindo mensagens de erro para o log
                    var errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                    _logger.LogWarning("Validação falhou para {RequestType}. Erros: {Errors}", typeof(TRequest).Name, string.Join(", ", errorMessages));

                    throw new ValidationException(failures); // Passa os objetos ValidationFailure
                }

            }

            // 2. Logging - Antes da execução
            _logger.LogInformation("Iniciando execução de {RequestType} com dados: {@Request}", typeof(TRequest).Name, request);

            try
            {
                // 3. Execução do handler
                var response = await next();

                // 4. Logging - Após a execução
                _logger.LogInformation("Execução de {RequestType} concluída com sucesso.", typeof(TRequest).Name);

                return response;
            }
            catch (Exception ex)
            {
                // 5. Logging - Em caso de erro
                _logger.LogError(ex, "Erro ao executar {RequestType}.", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
