using YMA.Models.Models;

namespace YMA.Business.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel> CreateAccountWithEmailAndPassword(AuthModel auth);

        Task<ResponseModel> SignInWithEmailAndPassword(AuthModel auth);
    }
}
