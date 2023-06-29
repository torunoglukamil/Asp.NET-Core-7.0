﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Models.Converters;
using YMA.Models.Entities;
using YMA.Models.Models;

namespace YMA.DataAccess.Repositories
{
    public class AccountRepository
    {
        private readonly ymaContext _db;
        private readonly AccountQuery _query;
        private readonly ResponseHelper _helper;
        private readonly IValidator<AccountModel> _validator;
        private readonly AccountConverter _converter;

        public AccountRepository(ymaContext db, AccountQuery query, ResponseHelper helper, IValidator<AccountModel> validator, AccountConverter converter)
        {
            _db = db;
            _query = query;
            _helper = helper;
            _validator = validator;
            _converter = converter;
        }

        public async Task<ResponseModel> CreateAccount(AccountModel account) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(account);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel emailResponse = _query.CheckIfEmailAlreadyInUse(account.email!, null);
               if (emailResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return emailResponse;
               }
               ResponseModel phoneResponse = _query.CheckIfPhoneAlreadyInUse(account.phone!, null);
               if (phoneResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   return phoneResponse;
               }
               account _account = _converter.ToAccount(account);
               _account.create_date = DateTime.Now;
               _account.is_disabled = false;
               _db.accounts.Add(_account);
               _db.SaveChanges();
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   message = "Hesap başarıyla oluşturuldu.",
               };
           });

        public async Task<ResponseModel> UpdateAccount(AccountModel account) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(account);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel accountResponse = _query.GetAccountById(account.id, false);
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
                   message = "Hesap bilgileri başarıyla güncellendi.",
               };
           });

        public ResponseModel DisableAccountById(int id) => _helper.TryCatch(
           () =>
           {
               ResponseModel accountResponse = _query.GetAccountById(id, false);
               if (accountResponse.status_code == StatusCodes.Status400BadRequest)
               {
                   if (accountResponse.type == "account-disabled")
                   {
                       accountResponse.message = "Hesap zaten devre dışı bırakıldı.";
                   }
                   return accountResponse;
               }
               account account = accountResponse.data!;
               account.is_disabled = true;
               _db.SaveChanges();
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   message = "Hesap başarıyla devre dışı bırakıldı.",
               };
           });
    }
}
