using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class AuthRepository
    {
        private readonly IAuthRepository _authRepository;
        private readonly AccountRepository _accountRepository;
        private readonly IValidator<CreateAccountModel> _createAccountValidator;

        public AuthRepository(IAuthRepository authRepository, AccountRepository accountRepository, IValidator<CreateAccountModel> createAccountValidator)
        {
            _authRepository = authRepository;
            _accountRepository = accountRepository;
            _createAccountValidator = createAccountValidator;
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount, AccountModel account) => await ResponseHelper.TryCatch(
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
               ResponseModel response = await _accountRepository.CreateAccountValidate(account);
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
