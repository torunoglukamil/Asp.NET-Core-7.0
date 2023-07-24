using FluentValidation;
using YMA.Entities.Models;

namespace YMA.Entities.Validators
{
    public class ReplyCompanyInviteValidator : AbstractValidator<ReplyCompanyInviteModel>
    {
        public ReplyCompanyInviteValidator()
        {
            RuleFor(x => x.id)
                .NotNull()
                .WithMessage("Davet ID geçersiz.")
                .NotEmpty()
                .WithMessage("Davet ID geçersiz.");
            RuleFor(x => x.is_accepted)
                .NotNull()
                .WithMessage("Davet yanıtı geçersiz.");
        }
    }
}
