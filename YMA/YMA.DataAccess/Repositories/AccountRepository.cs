using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Models.Models;

namespace YMA.DataAccess.Repositories
{
    public class AccountRepository
    {
        private readonly ymaContext _db;
        private readonly AccountQuery _query;
        private readonly ResponseHelper _helper;
        private readonly IValidator<account> _validator;

        public AccountRepository(ymaContext db, AccountQuery query, ResponseHelper helper, IValidator<account> validator)
        {
            _db = db;
            _query = query;
            _helper = helper;
            _validator = validator;
        }

        public async Task<ResponseModel> CreateAccount(account account) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(account);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       data = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel emailResponse = _query.CheckIfEmailAlreadyInUse(account.email!, account.id);
               if (emailResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return emailResponse;
               }
               ResponseModel phoneResponse = _query.CheckIfPhoneAlreadyInUse(account.phone!, account.id);
               if (phoneResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return phoneResponse;
               }
               account _account = new account()
               {
                   first_name = account.first_name,
                   last_name = account.last_name,
                   email = account.email,
                   phone = account.phone,
                   default_address_id = account.default_address_id,
                   create_date = DateTime.Now,
                   is_disabled = false,
               };
               _db.accounts.Add(_account);
               _db.SaveChanges();
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   data = "Hesap başarıyla oluşturuldu.",
               };
           });

        public async Task<ResponseModel> UpdateAccount(account account) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(account);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       data = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel accountResponse = _query.GetAccountById(account.id);
               if (accountResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return accountResponse;
               }
               ResponseModel phoneResponse = _query.CheckIfPhoneAlreadyInUse(account.phone!, account.id);
               if (phoneResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return phoneResponse;
               }
               account _account = accountResponse.data!;
               _account.first_name = account.first_name;
               _account.last_name = account.last_name;
               _account.phone = account.phone;
               _db.SaveChanges();
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   data = "Hesap bilgileri başarıyla güncellendi.",
               };
           });

        public ResponseModel DisableAccountById(int id) => _helper.TryCatch(
           () =>
           {
               ResponseModel accountResponse = _query.GetAccountById(id);
               if (accountResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   if (accountResponse.type == "account-disabled")
                   {
                       accountResponse.data = "Hesap zaten devre dışı bırakıldı.";
                       accountResponse.type = null;
                   }
                   return accountResponse;
               }
               account account = accountResponse.data!;
               account.is_disabled = true;
               _db.SaveChanges();
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   data = "Hesap başarıyla devre dışı bırakıldı.",
               };
           });
    }
}
