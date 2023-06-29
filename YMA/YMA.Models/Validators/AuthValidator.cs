using FluentValidation;
using System.Text.RegularExpressions;
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
                .WithMessage("Şifrenizi giriniz.")
                .MinimumLength(8)
                .When(x => x.is_creation == true)
                .WithMessage("Şifre en az 8 haneli olmalı.")
                .Matches(new Regex(@"^(?=.*[A-Za-z])"))
                .When(x => x.is_creation == true)
                .WithMessage("Şifre en az 1 tane harf içermeli.")
                .Matches(new Regex(@"^(?=.*[0-9])"))
                .When(x => x.is_creation == true)
                .WithMessage("Şifre en az 1 tane rakam içermeli.");
            RuleFor(x => x.password_again)
                .NotNull()
                .When(x => x.is_creation == true)
                .WithMessage("Girilen şifreler eşleşmiyor.")
                .NotEmpty()
                .When(x => x.is_creation == true)
                .WithMessage("Girilen şifreler eşleşmiyor.");
        }
    }
}
