using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class FirebaseAuthQuery : IAuthQuery
    {
        private readonly FirebaseAuthHelper _firebaseAuthHelper;

        public FirebaseAuthQuery(FirebaseAuthHelper firebaseAuthHelper)
        {
            _firebaseAuthHelper = firebaseAuthHelper;
        }

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await ResponseHelper.TryCatch(
           async () =>
           {
               try
               {
                   await _firebaseAuthHelper.FirebaseAuth.SignInWithEmailAndPasswordAsync(signInAccount.email, signInAccount.password);
               }
               catch (FirebaseAuthException e)
               {
                   return new ResponseModel()
                   {
                       status_code = StatusCodes.Status400BadRequest,
                       message = FirebaseAuthHelper.GetMessageByAuthErrorReason(e.Reason),
                   };
               }
               return new ResponseModel()
               {
                   status_code = StatusCodes.Status200OK,
               };
           }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(string email)
        {
            try
            {
                await _firebaseAuthHelper.FirebaseAuth.SendPasswordResetEmailAsync(email);
            }
            catch (Exception e)
            {
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status400BadRequest,
                    message = "Hesap bulunamadı.",
                    data = e.Message,
                };
            }
            return new ResponseModel()
            {
                status_code = StatusCodes.Status200OK,
            };
        }
    }
}
