using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using YMA.Business.Interfaces;
using YMA.DataAccess.Helpers;
using YMA.Models.Models;

namespace YMA.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthService _service;
        private readonly ResponseHelper _helper;
        private readonly IValidator<AuthModel> _validator;

        public AuthService(IAuthService service, ResponseHelper helper, IValidator<AuthModel> validator)
        {
            _service = service;
            _helper = helper;
            _validator = validator;
        }

        public async Task<ResponseModel> CreateAccountWithEmailAndPassword(AuthModel auth) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(auth);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel response = await _service.CreateAccountWithEmailAndPassword(auth);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   message = "Hesap başarıyla oluşturuldu."
               };
           });

        public async Task<ResponseModel> SignInWithEmailAndPassword(AuthModel auth) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(auth);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               ResponseModel response = await _service.SignInWithEmailAndPassword(auth);
               if (response.status_code == StatusCodes.Status400BadRequest)
               {
                   return response;
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   message = "Hesaba başarıyla giriş yapıldı."
               };
           });
    }
}
