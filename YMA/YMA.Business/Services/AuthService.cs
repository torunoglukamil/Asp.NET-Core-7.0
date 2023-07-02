using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.Business.Interfaces;
using YMA.DataAccess.Helpers;
using YMA.Models.Models;

namespace YMA.Business.Services
{
    public class AuthService
    {
        private readonly IAuthService _service;
        private readonly AccountService _accountService;
        private readonly ResponseHelper _helper;
        private readonly IValidator<CreateAccountModel> _createAccountValidator;
        private readonly IValidator<SignInAccountModel> _signInAccountValidator;
        private readonly IValidator<EmailModel> _emailValidator;

        public AuthService(IAuthService service, AccountService accountService, ResponseHelper helper, IValidator<CreateAccountModel> createAccountValidator, IValidator<SignInAccountModel> signInAccountValidator, IValidator<EmailModel> emailValidator)
        {
            _service = service;
            _accountService = accountService;
            _helper = helper;
            _createAccountValidator = createAccountValidator;
            _signInAccountValidator = signInAccountValidator;
            _emailValidator = emailValidator;
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount, AccountModel account) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _createAccountValidator.ValidateAsync(createAccount);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel response = await _accountService.Repository.CreateAccountValidate(account);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               response = await _service.CreateAccount(createAccount);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return _accountService.Repository.CreateAccount(account);
           }
        );

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _signInAccountValidator.ValidateAsync(signInAccount);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel response = await _service.SignInAccount(signInAccount);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return _accountService.Query.GetAccountByEmail(signInAccount.email!);
           }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(EmailModel email) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _emailValidator.ValidateAsync(email);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel response = await _service.SendPasswordResetEmail(email.email!);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   message = "Şifre sıfırlama bağlantısı e-posta adresinize gönderildi.",
               };
           }
        );
    }
}
