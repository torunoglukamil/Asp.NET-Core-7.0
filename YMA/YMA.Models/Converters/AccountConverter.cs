using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class AccountConverter
    {
        public static AccountModel ToModel(account account) => new()
        {
            id = account.id,
            first_name = account.first_name,
            last_name = account.last_name,
            email = account.email,
            phone = account.phone,
            default_address_id = account.default_address_id,
        };

        public static account ToAccount(AccountModel account) => new()
        {
            first_name = account.first_name,
            last_name = account.last_name,
            email = account.email,
            phone = account.phone,
            default_address_id = account.default_address_id,
        };
    }
}
