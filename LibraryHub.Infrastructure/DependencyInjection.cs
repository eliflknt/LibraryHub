using LibraryHub.Application.Interfaces;
using LibraryHub.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryHub.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}