using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using YMA.Business.Interfaces;
using YMA.DataAccess.Helpers;
using YMA.Models.Models;

namespace YMA.Business.Services
{
    public class FirebaseAuthService : IAuthService
    {
        private readonly FirebaseAuthProvider _firebaseAuth;
        private readonly ResponseHelper _helper;

        public FirebaseAuthService(IConfiguration config, ResponseHelper helper)
        {
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(config.GetValue<string>("FirebaseWebApiKey")));
            _helper = helper;
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
                    return "Bir hata oluştu. Lütfen sonra tekrar deneyin.";
            }
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount) => await _helper.TryCatch(
           async () =>
           {
               try
               {
                   await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(createAccount.email, createAccount.password);
               }
               catch (FirebaseAuthException e)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = GetMessageByAuthErrorReason(e.Reason),
                   };
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
               };
           }
        );

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await _helper.TryCatch(
           async () =>
           {
               try
               {
                   await _firebaseAuth.SignInWithEmailAndPasswordAsync(signInAccount.email, signInAccount.password);
               }
               catch (FirebaseAuthException e)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = GetMessageByAuthErrorReason(e.Reason),
                   };
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
               };
           }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(string email) => await _helper.TryCatch(
           async () =>
           {
               try
               {
                   await _firebaseAuth.SendPasswordResetEmailAsync(email);
               }
               catch (FirebaseAuthException e)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = GetMessageByAuthErrorReason(e.Reason),
                   };
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
               };
           }
        );
    }
}
