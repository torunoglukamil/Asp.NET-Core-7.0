using FluentValidation;
using System.Text.RegularExpressions;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class CreateAccountValidator : AbstractValidator<CreateAccountModel>
    {
        public CreateAccountValidator()
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
                .WithMessage("Şifrenizi giriniz.")
                .MinimumLength(8)
                .WithMessage("Şifre en az 8 haneli olmalı.")
                .Matches(new Regex(@"^(?=.*[A-Za-z])"))
                .WithMessage("Şifre en az 1 tane harf içermeli.")
                .Matches(new Regex(@"^(?=.*[0-9])"))
                .WithMessage("Şifre en az 1 tane rakam içermeli.")
                .Equal(x => x.password_again)
                .WithMessage("Girilen şifreler eşleşmiyor.");
        }
    }
}
