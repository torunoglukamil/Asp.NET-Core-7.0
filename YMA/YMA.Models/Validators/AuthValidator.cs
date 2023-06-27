using FluentValidation;
using YMA.Models.Models;

namespace YMA.Models.Validators
{
    public class AuthValidator : AbstractValidator<AuthModel>
    {
        public AuthValidator()
        {
            RuleFor(x => x.email)
                .NotNull()
                .WithMessage("E-posta adresinizi giriniz.")
                .NotEmpty()
                .WithMessage("E-posta adresinizi giriniz.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.");
            RuleFor(x => x.password)
                .NotNull()
                .WithMessage("Şifrenizi giriniz.")
                .NotEmpty()
                .WithMessage("Şifrenizi giriniz.");
        }
    }
}
