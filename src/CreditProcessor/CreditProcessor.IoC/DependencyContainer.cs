using CreditProcessor.Domain.Repositories;
using CreditProcessor.Domain.UnitOfWork;
using CreditProcessor.Infrastructure.Repositories;
using CreditProcessor.Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CreditProcessor.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(Domain.Commands.Handlers.ProcessCreditHandler)));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICreditRepository, CreditRepository>();
        }
    }
}