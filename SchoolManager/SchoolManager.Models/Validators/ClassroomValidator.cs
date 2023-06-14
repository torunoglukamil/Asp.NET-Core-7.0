using FluentValidation;
using SchoolManager.Models.Models;

namespace SchoolManager.Models.Validators
{
    public class ClassroomValidator : AbstractValidator<Classroom>
    {
        public ClassroomValidator()
        {
            RuleFor(x => x.grade)
                .NotNull()
                .GreaterThan(Convert.ToSByte(0))
                .WithMessage("Sınıf derecesi en az 1 olmalı.");
            RuleFor(x => x.branch)
                .NotNull()
                .NotEmpty()
                .WithMessage("Şube bilgisi eksik.")
                .Length(1)
                .WithMessage("Şube bilgisi 1 karakter olmalı.");
        }
    }
}
