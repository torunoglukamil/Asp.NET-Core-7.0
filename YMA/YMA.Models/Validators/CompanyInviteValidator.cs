using FluentValidation;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class CompanyInviteValidator : AbstractValidator<CompanyInviteModel>
    {
        public CompanyInviteValidator()
        {
            RuleFor(x => x.receiver_id)
                .NotNull()
                .WithMessage("Alıcı ID geçersiz.")
                .NotEmpty()
                .WithMessage("Alıcı ID geçersiz.");
            RuleFor(x => x.sender_id)
                .NotNull()
                .WithMessage("Gönderen ID geçersiz.")
                .NotEmpty()
                .WithMessage("Gönderen ID geçersiz.");
            RuleFor(x => x.is_buying)
                .NotNull()
                .WithMessage("Satın alma değeri geçersiz.");
            RuleFor(x => x.is_selling)
                .NotNull()
                .WithMessage("Satış yapma değeri geçersiz.");
            RuleFor(x => x.is_current_account_registration)
                .NotNull()
                .WithMessage("Cari hesap kaydı değeri geçersiz.");
        }
    }
}
