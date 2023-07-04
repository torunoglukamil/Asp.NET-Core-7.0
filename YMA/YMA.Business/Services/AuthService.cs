using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;

namespace YMA.Business.Services
{
    public class AuthService
    {
        private readonly AuthQuery _query;
        private readonly AuthRepository _repository;

        public AuthService(AuthQuery query, AuthRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public AuthQuery Query { get { return _query; } }

        public AuthRepository Repository { get { return _repository; } }
    }
}
