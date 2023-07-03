using FluentValidation;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class EmailValidator : AbstractValidator<EmailModel>
    {
        public EmailValidator()
        {
            RuleFor(x => x.email)
                .NotNull()
                .WithMessage("E-posta adresinizi giriniz.")
                .NotEmpty()
                .WithMessage("E-posta adresinizi giriniz.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.");
        }
    }
}
