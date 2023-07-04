using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class AccountQuery
    {
        private readonly ymaContext _db;

        public AccountQuery(ymaContext db)
        {
            _db = db;
        }

        public ResponseModel GetAccountById(int id, bool returnAccountModel) => ResponseHelper.TryCatch(
             () =>
             {
                 account? account = _db.accounts.Where(x => x.id == id).FirstOrDefault();
                 if (account == null)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         message = "Hesap bulunamadı.",
                     };
                 }
                 if (account.is_disabled ?? false)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         message = "Hesap devre dışı.",
                     };
                 }
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = returnAccountModel ? global::YMA.Entities.Converters.AccountConverter.ToModel(account) : account,
                 };
             }
          );

        public ResponseModel GetAccountByEmail(string email) => ResponseHelper.TryCatch(
             () =>
             {
                 account? account = _db.accounts.Where(x => x.email == email).FirstOrDefault();
                 if (account == null)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         message = "Hesap bulunamadı.",
                     };
                 }
                 if (account.is_disabled ?? false)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         message = "Hesap devre dışı.",
                     };
                 }
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = AccountConverter.ToModel(account),
                 };
             }
          );

        public ResponseModel CheckIfEmailAlreadyInUse(String email, int? accountId)
        {
            account? account = _db.accounts.Where(x => x.email == email).FirstOrDefault();
            if (account != null && (account.id != accountId))
            {
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status400BadRequest,
                    message = "E-posta adresi zaten kullanımda.",
                };
            }
            return new ResponseModel()
            {
                status_code = StatusCodes.Status200OK,
            };
        }

        public ResponseModel CheckIfPhoneAlreadyInUse(String? phone, int? accountId)
        {
            if (phone != null)
            {
                account? account = _db.accounts.Where(x => x.phone == phone).FirstOrDefault();
                if (account != null && (account.id != accountId))
                {
                    return new ResponseModel()
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        message = "Telefon numarası zaten kullanımda.",
                    };
                }
            }
            return new ResponseModel()
            {
                status_code = StatusCodes.Status200OK,
            };
        }
    }
}
