using YMA.Entities.Models;

namespace YMA.Business.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel> CreateAccount(CreateAccountModel createAccount);

        Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount);

        Task<ResponseModel> SendPasswordResetEmail(string email);
    }
}
