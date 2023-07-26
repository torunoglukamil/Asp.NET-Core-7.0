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
        private readonly ResponseHelper _responseHelper;

        public AccountQuery(ymaContext db, ResponseHelper responseHelper)
        {
            _db = db;
            _responseHelper = responseHelper;
        }

        public ResponseModel GetAccountById(string accountId, bool returnDbAccount = false) => _responseHelper.TryCatch(
            "AccountQuery.GetAccountById",
            () =>
            {
                account? account = _db.accounts.Where(x => x.id == accountId).FirstOrDefault();
                if (account == null)
                {
                    return new ResponseModel()
                    {
                        message = "Hesap bulunamadı.",
                    };
                }
                if (account.is_disabled ?? false)
                {
                    return new ResponseModel()
                    {
                        message = "Hesap devre dışı.",
                    };
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    data = returnDbAccount ? account : AccountConverter.ToModel(account),
                };
            }
          );

        public ResponseModel GetAccountByEmail(string email) => _responseHelper.TryCatch(
            "AccountQuery.GetAccountByEmail",
            () =>
            {
                account? account = _db.accounts.Where(x => x.email == email).FirstOrDefault();
                if (account == null)
                {
                    return new ResponseModel()
                    {
                        message = "Hesap bulunamadı.",
                    };
                }
                if (account.is_disabled ?? false)
                {
                    return new ResponseModel()
                    {
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

        public ResponseModel CheckIfEmailAlreadyInUse(string email, string? accountId)
        {
            account? account = _db.accounts.Where(x => x.email == email).FirstOrDefault();
            if (account != null && (account.id != accountId))
            {
                return new ResponseModel()
                {
                    message = "E-posta adresi zaten kullanımda.",
                };
            }
            return new ResponseModel()
            {
                status_code = StatusCodes.Status200OK,
            };
        }

        public ResponseModel CheckIfPhoneAlreadyInUse(string? phone, string? accountId)
        {
            if (phone != null)
            {
                account? account = _db.accounts.Where(x => x.phone == phone).FirstOrDefault();
                if (account != null && (account.id != accountId))
                {
                    return new ResponseModel()
                    {
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
