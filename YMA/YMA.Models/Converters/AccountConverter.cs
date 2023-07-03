using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.Entities.Converters
{
    public class AccountConverter
    {
        public AccountModel ToAccountModel(account account) => new AccountModel()
        {
            id = account.id,
            first_name = account.first_name,
            last_name = account.last_name,
            email = account.email,
            phone = account.phone,
            default_address_id = account.default_address_id,
        };

        public account ToAccount(AccountModel account) => new account()
        {
            first_name = account.first_name,
            last_name = account.last_name,
            email = account.email,
            phone = account.phone,
            default_address_id = account.default_address_id,
        };
    }
}
