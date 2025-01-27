using DigitalWalletAPI.Application.Commands.User;
using DigitalWalletAPI.Application.Interfaces;
using DigitalWalletAPI.Application.Interfaces.Hasher;
using DigitalWalletAPI.Application.Interfaces.Repositories;
using DigitalWalletAPI.Application.Interfaces.Repositories.Application.Interfaces.Repositories;
using DigitalWalletAPI.Application.Services;
using DigitalWalletAPI.Infrastructure.Database;
using DigitalWalletAPI.Infrastructure.Database.UnitOfWork;
using DigitalWalletAPI.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DigitalWalletAPI.IoC
{
    public static class DependecyInjector
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
                Assembly.Load("DigitalWalletAPI.Application")
            ));
            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DigitalWalletContext>(options =>
             options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));
           
            // Aplicar migrações automaticamente
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<DigitalWalletContext>();
                    context.Database.Migrate();
                }
            }

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            // Outros serviços...
            return services;
        }
    }
}
