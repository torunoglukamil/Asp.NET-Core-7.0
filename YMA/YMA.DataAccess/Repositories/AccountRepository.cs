using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;
using YMA.Entities.Validators;

namespace YMA.DataAccess.Repositories
{
    public class AccountRepository
    {
        private readonly ymaContext _db;
        private readonly AccountQuery _accountQuery;
        private readonly ResponseHelper _responseHelper;
        private readonly ValidationHelper<AccountModel> _accountValidator;

        public AccountRepository(ymaContext db, AccountQuery accountQuery, ResponseHelper responseHelper)
        {
            _db = db;
            _accountQuery = accountQuery;
            _responseHelper = responseHelper;
            _accountValidator = new ValidationHelper<AccountModel>(new AccountValidator());
        }

        public ResponseModel CreateAccountValidate(AccountModel account) => _responseHelper.TryCatch(
            "AccountRepository.CreateAccountValidate",
            () =>
            {
                ResponseModel response = _accountValidator.Validate(account);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _accountQuery.CheckIfEmailAlreadyInUse(account.email!, null);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _accountQuery.CheckIfPhoneAlreadyInUse(account.phone!, null);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            }
        );

        public ResponseModel CreateAccount(AccountModel account) => _responseHelper.TryCatch(
            "AccountRepository.CreateAccount",
            () =>
            {
                _db.accounts.Add(AccountConverter.ToAccount(account));
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Hesap başarıyla oluşturuldu.",
                };
            }
        );

        public ResponseModel UpdateAccount(AccountModel account) => _responseHelper.TryCatch(
            "AccountRepository.UpdateAccount",
            () =>
            {
                ResponseModel response = _accountValidator.Validate(account);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _accountQuery.GetAccountById(account.id!, true);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                account _account = response.data!;
                response = _accountQuery.CheckIfPhoneAlreadyInUse(account.phone!, account.id);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                _account.first_name = account.first_name;
                _account.last_name = account.last_name;
                _account.phone = account.phone;
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Hesap bilgileri başarıyla güncellendi.",
                };
            }
        );

        public ResponseModel DisableAccount(string accountId) => _responseHelper.TryCatch(
            "AccountRepository.DisableAccount",
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
                        message = "Hesap zaten devre dışı.",
                    };
                }
                account.is_disabled = true;
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Hesap başarıyla devre dışı bırakıldı.",
                };
            }
        );

        public ResponseModel ActivateAccount(string accountId) => _responseHelper.TryCatch(
            "AccountRepository.ActivateAccount",
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
                if (!(account.is_disabled ?? false))
                {
                    return new ResponseModel()
                    {
                        message = "Hesap zaten aktif.",
                    };
                }
                account.is_disabled = false;
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Hesap başarıyla aktifleştirildi.",
                };
            }
        );
    }
}
