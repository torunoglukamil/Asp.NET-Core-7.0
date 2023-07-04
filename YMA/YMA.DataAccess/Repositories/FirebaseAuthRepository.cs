using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        private readonly FirebaseAuthHelper _firebaseAuthHelper;

        public FirebaseAuthRepository(FirebaseAuthHelper firebaseAuthHelper)
        {
            _firebaseAuthHelper = firebaseAuthHelper;
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount) => await ResponseHelper.TryCatch(
            async () =>
            {
                try
                {
                    await _firebaseAuthHelper.FirebaseAuth.CreateUserWithEmailAndPasswordAsync(createAccount.email, createAccount.password);
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
    }
}
