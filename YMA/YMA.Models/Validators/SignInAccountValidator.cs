﻿using FluentValidation;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class SignInAccountValidator : AbstractValidator<SignInAccountModel>
    {
        public SignInAccountValidator()
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