﻿using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Queries;
using YMA.Entities.Converters;
using YMA.Entities.Entities;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class AccountRepository
    {
        private readonly ymaContext _db;
        private readonly AccountQuery _accountQuery;
        private readonly ResponseHelper _responseHelper;
        private readonly IValidator<AccountModel> _accountValidator;

        public AccountRepository(ymaContext db, AccountQuery accountQuery, ResponseHelper responseHelper, IValidator<AccountModel> accountValidator)
        {
            _db = db;
            _accountQuery = accountQuery;
            _responseHelper = responseHelper;
            _accountValidator = accountValidator;
        }

        public async Task<ResponseModel> CreateAccountValidate(AccountModel account) => await _responseHelper.TryCatch(
            "AccountRepository.CreateAccountValidate",
            async () =>
            {
                ValidationResult validationResult = await _accountValidator.ValidateAsync(account);
                if (!validationResult.IsValid)
                {
                    return new ResponseModel()
                    {
                        message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                    };
                }
                ResponseModel emailResponse = _accountQuery.CheckIfEmailAlreadyInUse(account.email!, null);
                if (emailResponse.status_code == StatusCodes.Status400BadRequest)
                {
                    return emailResponse;
                }
                ResponseModel phoneResponse = _accountQuery.CheckIfPhoneAlreadyInUse(account.phone!, null);
                if (phoneResponse.status_code == StatusCodes.Status400BadRequest)
                {
                    return phoneResponse;
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
                account _account = AccountConverter.ToAccount(account);
                _account.create_date = DateTime.Now;
                _account.is_disabled = false;
                _db.accounts.Add(_account);
                _db.SaveChanges();
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                    message = "Hesap başarıyla oluşturuldu.",
                };
            }
        );

        public async Task<ResponseModel> UpdateAccount(AccountModel account) => await _responseHelper.TryCatch(
            "AccountRepository.UpdateAccount",
            async () =>
            {
                ValidationResult validationResult = await _accountValidator.ValidateAsync(account);
                if (!validationResult.IsValid)
                {
                    return new ResponseModel()
                    {
                        message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                    };
                }
                ResponseModel accountResponse = _accountQuery.GetAccountById(account.id, true);
                if (accountResponse.status_code == StatusCodes.Status400BadRequest)
                {
                    return accountResponse;
                }
                ResponseModel phoneResponse = _accountQuery.CheckIfPhoneAlreadyInUse(account.phone!, account.id);
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
            }
        );

        public ResponseModel DisableAccount(int id) => _responseHelper.TryCatch(
            "AccountRepository.DisableAccount",
            () =>
            {
                account? account = _db.accounts.Where(x => x.id == id).FirstOrDefault();
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

        public ResponseModel ActivateAccount(int id) => _responseHelper.TryCatch(
            "AccountRepository.ActivateAccount",
            () =>
            {
                account? account = _db.accounts.Where(x => x.id == id).FirstOrDefault();
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
