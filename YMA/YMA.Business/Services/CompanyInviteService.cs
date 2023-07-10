using YMA.DataAccess.Queries;
using YMA.DataAccess.Repositories;

namespace YMA.Business.Services
{
    public class CompanyInviteService
    {
        private readonly CompanyInviteQuery _query;
        private readonly CompanyInviteRepository _repository;

        public CompanyInviteService(CompanyInviteQuery query, CompanyInviteRepository repository)
        {
            _query = query;
            _repository = repository;
        }

        public CompanyInviteQuery Query { get { return _query; } }

        public CompanyInviteRepository Repository { get { return _repository; } }
    }
}
