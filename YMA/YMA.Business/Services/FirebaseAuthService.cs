using Firebase.Auth;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using YMA.Business.Interfaces;
using YMA.DataAccess.Helpers;
using YMA.Models.Models;

namespace YMA.Business.Services
{
    public class FirebaseAuthService : IAuthService
    {
        private readonly FirebaseAuthProvider _auth;
        private readonly ResponseHelper _helper;
        private readonly IValidator<AuthModel> _validator;

        public FirebaseAuthService(IConfiguration config, ResponseHelper helper, IValidator<AuthModel> validator)
        {
            _auth = new FirebaseAuthProvider(new FirebaseConfig(config.GetValue<string>("FirebaseWebApiKey")));
            _helper = helper;
            _validator = validator;
        }

        private String GetMessageByAuthErrorReason(AuthErrorReason reason)
        {
            switch (reason)
            {
                case AuthErrorReason.UserDisabled:
                    return "Hesap devre dışı.";
                case AuthErrorReason.InvalidEmailAddress:
                    return "Hesap bulunamadı.";
                case AuthErrorReason.WeakPassword:
                    return "Lütfen daha güçlü bir şifre oluşturunuz.";
                case AuthErrorReason.EmailExists:
                    return "E-posta adresi zaten kullanımda.";
                case AuthErrorReason.UnknownEmailAddress:
                    return "Hesap bulunamadı.";
                case AuthErrorReason.WrongPassword:
                    return "Şifrenizi yanlış girdiniz.";
                default:
                    return "Bir hata oldu. Lütfen sonra tekrar deneyin.";
            }
        }

        public async Task<ResponseModel> SignInWithEmailAndPassword(AuthModel authModel) => await _helper.TryCatch(
           async () =>
           {
               ValidationResult validationResult = await _validator.ValidateAsync(authModel);
               if (!validationResult.IsValid)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       data = validationResult.Errors.FirstOrDefault()!.ErrorMessage,
                   };
               }
               try
               {
                   await _auth.SignInWithEmailAndPasswordAsync(authModel.email, authModel.password);
               }
               catch (FirebaseAuthException e)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       data = GetMessageByAuthErrorReason(e.Reason),
                   };
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
                   data = "Hesaba başarıyla giriş yapıldı."
               };
           });
    }
}
