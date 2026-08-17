using FluentValidation;
using LibraryHub.Application.Services;
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

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<ILoanService, LoanService>();

            return services;
        }
    }
}