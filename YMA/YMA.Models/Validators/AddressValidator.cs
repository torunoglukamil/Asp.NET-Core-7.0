using FluentValidation;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class AddressValidator : AbstractValidator<AddressModel>
    {
        public AddressValidator() {
            RuleFor(x => x.full_address)
                .NotNull()
                .WithMessage("Tam adres giriniz.")
                .NotEmpty()
                .WithMessage("Tam adres giriniz.");
            RuleFor(x => x.province)
                .NotNull()
                .WithMessage("İl seçiniz.")
                .NotEmpty()
                .WithMessage("İl seçiniz.");
            RuleFor(x => x.district)
                .NotNull()
                .WithMessage("İlçe seçiniz.")
                .NotEmpty()
                .WithMessage("İlçe seçiniz.");
            RuleFor(x => x.neighbourhood)
                .NotNull()
                .WithMessage("Mahalle seçiniz.")
                .NotEmpty()
                .WithMessage("Mahalle seçiniz.");
        }
    }
}
