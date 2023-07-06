using Microsoft.AspNetCore.Http;
using YMA.DataAccess.Helpers;
using YMA.Entities.Interfaces;
using YMA.Entities.Models;

namespace YMA.DataAccess.Repositories
{
    public class FirebaseAuthRepository : IAuthRepository
    {
        private readonly FirebaseAuthHelper _firebaseAuthHelper;
        private readonly ResponseHelper _responseHelper;

        public FirebaseAuthRepository(FirebaseAuthHelper firebaseAuthHelper, ResponseHelper responseHelper)
        {
            _firebaseAuthHelper = firebaseAuthHelper;
            _responseHelper = responseHelper;
        }

        public async Task<ResponseModel> CreateAccount(CreateAccountModel createAccount) => await _responseHelper.TryCatch(
            "FirebaseAuthRepository.CreateAccount",
            async () =>
            {
                await _firebaseAuthHelper.FirebaseAuth.CreateUserWithEmailAndPasswordAsync(createAccount.email, createAccount.password);
                return new ResponseModel()
                {
                    status_code = StatusCodes.Status200OK,
                };
            }
        );
    }
}
