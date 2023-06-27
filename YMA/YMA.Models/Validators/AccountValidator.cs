using FluentValidation;
using System.Text.RegularExpressions;
using YMA.Models.Models;

namespace YMA.Models.Validators
{
    public class AccountValidator : AbstractValidator<account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.first_name)
                .NotNull()
                .WithMessage("Kullanıcı ismi eksik.")
                .NotEmpty()
                .WithMessage("Kullanıcı ismi eksik.");
            RuleFor(x => x.last_name)
                .NotNull()
                .WithMessage("Kullanıcı soyismi eksik.")
                .NotEmpty()
                .WithMessage("Kullanıcı soyismi eksik.");
            RuleFor(x => x.email)
                .NotNull()
                .WithMessage("E-posta adresi eksik.")
                .NotEmpty()
                .WithMessage("E-posta adresi eksik.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.");
            RuleFor(x => x.phone)
                .Matches(new Regex(@"^[1-9]{1}[0-9]{9}$"))
                .When(x => x.phone != null)
                .WithMessage("Telefon numarası 5XX XXX XXXX formatında olmalı.");
        }
    }
}
