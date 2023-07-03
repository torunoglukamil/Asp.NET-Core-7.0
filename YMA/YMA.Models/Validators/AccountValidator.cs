using FluentValidation;
using System.Text.RegularExpressions;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class AccountValidator : AbstractValidator<AccountModel>
    {
        public AccountValidator()
        {
            RuleFor(x => x.first_name)
                .NotNull()
                .WithMessage("İsminizi giriniz.")
                .NotEmpty()
                .WithMessage("İsminizi giriniz.");
            RuleFor(x => x.last_name)
                .NotNull()
                .WithMessage("Soyisminizi giriniz.")
                .NotEmpty()
                .WithMessage("Soyisminizi giriniz.");
            RuleFor(x => x.email)
                .NotNull()
                .WithMessage("E-posta adresinizi giriniz.")
                .NotEmpty()
                .WithMessage("E-posta adresinizi giriniz.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.");
            RuleFor(x => x.phone)
                .NotEmpty()
                .When(x => x.phone != null)
                .WithMessage("Telefon numaranızı giriniz.")
                .Matches(new Regex(@"^[1-9]{1}[0-9]{9}$"))
                .When(x => x.phone != null)
                .WithMessage("Telefon numarası 5XX XXX XXXX formatında olmalı.");
        }
    }
}
