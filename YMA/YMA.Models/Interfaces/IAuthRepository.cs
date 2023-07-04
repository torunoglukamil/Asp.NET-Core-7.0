using YMA.Entities.Models;

namespace YMA.Entities.Interfaces
{
    public interface IAuthRepository
    {
        Task<ResponseModel> CreateAccount(CreateAccountModel createAccount);
    }
}
