using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Repositories;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;
using YMA.Entities.Validators;

namespace YMA.DataAccess.Queries
{
    public class AuthQuery
    {
        private readonly IAuthQuery _authQuery;
        private readonly AccountQuery _accountQuery;
        private readonly LogRepository _logRepository;
        private readonly ResponseHelper _responseHelper;
        private readonly ValidationHelper<SignInAccountModel> _signInAccountValidator;
        private readonly ValidationHelper<EmailModel> _emailValidator;

        public AuthQuery(IAuthQuery authQuery, AccountQuery accountQuery, LogRepository logRepository, ResponseHelper responseHelper)
        {
            _authQuery = authQuery;
            _accountQuery = accountQuery;
            _logRepository = logRepository;
            _responseHelper = responseHelper;
            _signInAccountValidator = new ValidationHelper<SignInAccountModel>(new SignInAccountValidator());
            _emailValidator = new ValidationHelper<EmailModel>(new EmailValidator());
        }

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await _responseHelper.TryCatch(
            "AuthQuery.SignInAccount",
            async () =>
            {
                ResponseModel response = _signInAccountValidator.Validate(signInAccount);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = await _authQuery.SignInAccount(signInAccount);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _accountQuery.GetAccountByEmail(signInAccount.email!);
                _logRepository.CreateLog(new LogModel()
                {
                    type = "login",
                    data = (response.data as AccountModel)!.id.ToString(),
                });
                return response;
            }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(EmailModel email) => await _responseHelper.TryCatch(
            "AuthQuery.SendPasswordResetEmail",
            async () =>
            {
                ResponseModel response = _emailValidator.Validate(email);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = await _authQuery.SendPasswordResetEmail(email.email!);
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
