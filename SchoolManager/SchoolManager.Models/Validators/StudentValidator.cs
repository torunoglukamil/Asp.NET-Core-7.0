using FluentValidation;
using SchoolManager.Models.Models;
using System.Text.RegularExpressions;

namespace SchoolManager.Models.Validators
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(x => x.first_name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Öğrenci ismi eksik.");
            RuleFor(x => x.last_name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Öğrenci soyismi eksik.");
            RuleFor(x => x.age)
                .NotNull()
                .WithMessage("Öğrenci yaşı eksik.")
                .GreaterThan(17)
                .WithMessage("Öğrenci yaşı en az 18 olmalı.");
            RuleFor(x => x.email)
                .NotNull()
                .NotEmpty()
                .WithMessage("E-posta adresi eksik.")
                .EmailAddress()
                .WithMessage("E-posta adresi doğru formatta değil.");
            RuleFor(x => x.phone)
                .Matches(new Regex(@"^[1-9]{1}[0-9]{9}$"))
                .When(x => x.phone != null)
                .WithMessage("Telefon numarası 5XX XXX XXXX formatında olmalı.");
            RuleFor(x => x.classroom_id)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Sınıf bilgisi eksik.");
        }
    }
}
