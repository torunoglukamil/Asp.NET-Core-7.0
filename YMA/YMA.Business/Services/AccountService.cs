using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;

namespace YMA.Business.Services
{
    public class AccountService
    {
        private readonly AccountQuery _query;
        private readonly AccountRepository _repository;

        public AccountService(AccountQuery query, AccountRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public AccountQuery Query { get { return _query; } }

        public AccountRepository Repository { get { return _repository; } }
    }
}
