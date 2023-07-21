using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Providers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class FirebaseAuthQuery : IAuthQuery
    {
        private readonly FirebaseAuthProvider _firebaseAuth;
        private readonly ResponseHelper _responseHelper;

        public FirebaseAuthQuery(FirebaseAuthenticationProvider firebaseAuthenticationProvider, ResponseHelper responseHelper)
        {
            _firebaseAuth = firebaseAuthenticationProvider.FirebaseAuth;
            _responseHelper = responseHelper;
        }

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await _responseHelper.TryCatch(
            "FirebaseAuthQuery.SignInAccount",
            async () =>
            {
                await _firebaseAuth.SignInWithEmailAndPasswordAsync(signInAccount.email, signInAccount.password);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            }
        );

        public async Task<ResponseModel> SendPasswordResetEmail(string email) => await _responseHelper.TryCatch(
            "FirebaseAuthQuery.SendPasswordResetEmail",
            async () =>
            {
                await _firebaseAuth.SendPasswordResetEmailAsync(email);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            },
            "Hesap bulunamadı."
        );
    }
}
