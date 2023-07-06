using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Queries
{
    public class FirebaseAuthQuery : IAuthQuery
    {
        private readonly FirebaseAuthHelper _firebaseAuthHelper;
        private readonly ResponseHelper _responseHelper;

        public FirebaseAuthQuery(FirebaseAuthHelper firebaseAuthHelper, ResponseHelper responseHelper)
        {
            _firebaseAuthHelper = firebaseAuthHelper;
            _responseHelper = responseHelper;
        }

        public async Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount) => await _responseHelper.TryCatch(
            "FirebaseAuthQuery.SignInAccount",
            async () =>
            {
                await _firebaseAuthHelper.FirebaseAuth.SignInWithEmailAndPasswordAsync(signInAccount.email, signInAccount.password);
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
                await _firebaseAuthHelper.FirebaseAuth.SendPasswordResetEmailAsync(email);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            },
            "Hesap bulunamadı."
        );
    }
}
