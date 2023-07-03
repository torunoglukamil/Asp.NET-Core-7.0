using YMA.Entities.Models;

namespace YMA.Business.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel> CreateAccount(CreateAccountModel account);

        Task<ResponseModel> SignInAccount(SignInAccountModel account);

        Task<ResponseModel> SendPasswordResetEmail(string email);
    }
}
