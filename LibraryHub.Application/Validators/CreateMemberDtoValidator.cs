using FluentValidation;
using LibraryHub.Application.DTOs;

namespace LibraryHub.Application.Validators
{
    public class CreateMemberDtoValidator : AbstractValidator<CreateMemberDto>
    {
        public CreateMemberDtoValidator()
        {
            RuleFor(x => x.AdSoyad)
                .NotEmpty()
                .WithMessage("Ad soyad boş bırakılamaz.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi girilmelidir.");

            RuleFor(x => x.Telefon)
                .NotEmpty()
                .WithMessage("Telefon numarası boş bırakılamaz.");
        }
    }
}