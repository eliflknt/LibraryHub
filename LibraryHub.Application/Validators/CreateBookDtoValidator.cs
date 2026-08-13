using FluentValidation;
using LibraryHub.Application.DTOs;

namespace LibraryHub.Application.Validators
{
    public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookDtoValidator()
        {
            RuleFor(x => x.Baslik)
                .NotEmpty()
                .WithMessage("Kitap başlığı boş bırakılamaz.");

            RuleFor(x => x.StokAdedi)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok adedi negatif olamaz.");

            RuleFor(x => x.RaftakiAdet)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Raftaki adet negatif olamaz.");
        }
    }
}