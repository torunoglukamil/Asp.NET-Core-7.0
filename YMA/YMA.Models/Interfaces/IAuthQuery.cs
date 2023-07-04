using YMA.Entities.Models;

namespace YMA.Entities.Interfaces
{
    public interface IAuthQuery
    {
        Task<ResponseModel> SignInAccount(SignInAccountModel signInAccount);

        Task<ResponseModel> SendPasswordResetEmail(string email);
    }
}
