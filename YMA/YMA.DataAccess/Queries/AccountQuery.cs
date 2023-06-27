using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Models.Models;

namespace YMA.DataAccess.Queries
{
    public class AccountQuery
    {
        private readonly ymaContext _db;
        private readonly ResponseHelper _helper;

        public AccountQuery(ymaContext db, ResponseHelper helper)
        {
            _db = db;
            _helper = helper;
        }

        public ResponseModel GetAccountById(int id) => _helper.TryCatch(
             () =>
             {
                 account? account = _db.accounts.Where(x => x.id == id).FirstOrDefault();
                 if (account == null)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         data = "Hesap bulunamadı.",
                     };
                 }
                 if (account.is_disabled ?? false)
                 {
                     return new ResponseModel()
                     {
                         status_code = StatusCodes.Status400BadRequest,
                         type = "account-disabled",
                         data = "Hesap devre dışı bırakıldı.",
                     };
                 }
                 return new ResponseModel()
                 {
                     status_code = StatusCodes.Status200OK,
                     data = account,
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
                    data = "E-posta adresi zaten kullanımda.",
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
                        data = "Telefon numarası zaten kullanımda.",
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
