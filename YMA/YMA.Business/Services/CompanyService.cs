using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class CompanyService
    {
        private readonly CompanyQuery _query;

        public CompanyService(CompanyQuery query)
        {
            _query = query;
        }

        public CompanyQuery Query { get { return _query; } }
    }
}
