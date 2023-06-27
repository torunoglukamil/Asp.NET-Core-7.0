using YMA.Models.Models;

namespace YMA.Business.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseModel> SignInWithEmailAndPassword(AuthModel authModel);
    }
}
