using Firebase.Auth;
using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.DataAccess.Providers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        private readonly FirebaseAuthProvider _firebaseAuth;
        private readonly ResponseHelper _responseHelper;

        public FirebaseAuthRepository(FirebaseAuthenticationProvider firebaseAuthenticationProvider, ResponseHelper responseHelper)
        {
            _firebaseAuth = firebaseAuthenticationProvider.FirebaseAuth;
            _responseHelper = responseHelper;
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount) => await _responseHelper.TryCatch(
            "FirebaseAuthRepository.CreateAccount",
            async () =>
            {
                await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(createAccount.email, createAccount.password);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            }
        );
    }
}
