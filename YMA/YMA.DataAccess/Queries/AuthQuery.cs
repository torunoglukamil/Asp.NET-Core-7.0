using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class AuthQuery
    {
        private readonly IAuthQuery _authQuery;
        private readonly AccountQuery _accountQuery;
        private readonly IValidator<SignInAccountModel> _signInAccountValidator;
        private readonly IValidator<EmailModel> _emailValidator;

        public AuthQuery(IAuthQuery authQuery, AccountQuery accountQuery, IValidator<SignInAccountModel> signInAccountValidator, IValidator<EmailModel> emailValidator)
        {
            _authQuery = authQuery;
            _accountQuery = accountQuery;
            _signInAccountValidator = signInAccountValidator;
            _emailValidator = emailValidator;
        }

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await ResponseHelper.TryCatch(
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
               ResponseModel response = await _authQuery.SignInAccount(signInAccount);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return _accountQuery.GetAccountByEmail(signInAccount.email!);
           }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(EmailModel email) => await ResponseHelper.TryCatch(
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
               ResponseModel response = await _authQuery.SendPasswordResetEmail(email.email!);
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
