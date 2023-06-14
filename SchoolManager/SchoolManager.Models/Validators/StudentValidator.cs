using FluentValidation;
using SchoolManager.Models.Models;
using System.Text.RegularExpressions;

namespace SchoolManager.Models.Validators
{
    public class StudentValidator : AbstractValidator<student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.first_name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Öğrenci ismi eksik.")
                .MaximumLength(50)
                .WithMessage("Öğrenci ismi en fazla 50 karakter olmalı.");
            RuleFor(x => x.last_name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Öğrenci soyismi eksik.")
                .MaximumLength(50)
                .WithMessage("Öğrenci soyismi en fazla 50 karakter olmalı.");
            RuleFor(x => x.age)
                .NotNull()
                .GreaterThan(Convert.ToSByte(17))
                .WithMessage("Öğrenci yaşı en az 18 olmalı.");
            RuleFor(x => x.email)
                .NotNull()
                .NotEmpty()
                .WithMessage("E-posta adresi eksik.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.")
                .MaximumLength(100)
                .WithMessage("E-posta adresi en fazla 100 karakter olmalı.");
            RuleFor(x => x.phone)
                .Matches(new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$"))
                .WithMessage("Telefon numarası 5XX XXX XXXX formatında olmalı.");
        }
    }
}
