using FluentValidation;
using YMA.Models.Models;

namespace YMA.Models.Validators
{
    public class AddressValidator : AbstractValidator<address>
    {
        public AddressValidator() {
            RuleFor(x => x.full_address)
                .NotNull()
                .WithMessage("Tam adres bilgisi eksik.")
                .NotEmpty()
                .WithMessage("Tam adres bilgisi eksik.");
            RuleFor(x => x.province)
                .NotNull()
                .WithMessage("İl bilgisi eksik.")
                .NotEmpty()
                .WithMessage("İl bilgisi eksik.");
            RuleFor(x => x.district)
                .NotNull()
                .WithMessage("İlçe bilgisi eksik.")
                .NotEmpty()
                .WithMessage("İlçe bilgisi eksik.");
            RuleFor(x => x.neighbourhood)
                .NotNull()
                .WithMessage("Mahalle bilgisi eksik.")
                .NotEmpty()
                .WithMessage("Mahalle bilgisi eksik.");
        }
    }
}
