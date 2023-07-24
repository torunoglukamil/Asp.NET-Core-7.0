using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class CompanyInviteConverter
    {
        public static CompanyInviteModel ToModel(company_invite company_invite) => new()
        {
            id = company_invite.id,
            receiver_id = company_invite.receiver_id,
            sender_id = company_invite.sender_id,
            is_buying = company_invite.is_buying,
            is_selling = company_invite.is_selling,
            is_current_account_registration = company_invite.is_current_account_registration,
            is_accepted = company_invite.is_accepted,
        };

        public static company_invite ToCompanyInvite(CompanyInviteModel company_invite) => new()
        {
            id = Guid.NewGuid().ToString(),
            receiver_id = company_invite.receiver_id,
            sender_id = company_invite.sender_id,
            is_buying = company_invite.is_buying,
            is_selling = company_invite.is_selling,
            is_current_account_registration = company_invite.is_current_account_registration,
            create_date = DateTime.Now,
        };
    }
}
