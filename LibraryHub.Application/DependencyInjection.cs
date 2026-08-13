using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryHub.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            services.AddScoped<IValidator<DTOs.CreateBookDto>, Validators.CreateBookDtoValidator>();
            services.AddScoped<IValidator<DTOs.CreateMemberDto>, Validators.CreateMemberDtoValidator>();

            return services;
        }
    }
}