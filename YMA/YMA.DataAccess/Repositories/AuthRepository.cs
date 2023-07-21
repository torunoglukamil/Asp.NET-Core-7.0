using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;
using YMA.Entities.Validators;

namespace YMA.DataAccess.Repositories
{
    public class AuthRepository
    {
        private readonly IAuthRepository _authRepository;
        private readonly AccountRepository _accountRepository;
        private readonly ResponseHelper _responseHelper;
        private readonly ValidationHelper<CreateAccountModel> _createAccountValidator;

        public AuthRepository(IAuthRepository authRepository, AccountRepository accountRepository, ResponseHelper responseHelper)
        {
            _authRepository = authRepository;
            _accountRepository = accountRepository;
            _responseHelper = responseHelper;
            _createAccountValidator = new ValidationHelper<CreateAccountModel>(new CreateAccountValidator());
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount, AccountModel account) => await _responseHelper.TryCatch(
            "AuthRepository.CreateAccount",
            async () =>
            {
                ResponseModel response = _createAccountValidator.Validate(createAccount);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _createAccountValidator.Validate(createAccount);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = _accountRepository.CreateAccountValidate(account);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                response = await _authRepository.CreateAccount(createAccount);
                if (response.status_code == StatusCodes.Status400BadRequest)
                {
                    return response;
                }
                return _accountRepository.CreateAccount(account);
            }
        );
    }
}
