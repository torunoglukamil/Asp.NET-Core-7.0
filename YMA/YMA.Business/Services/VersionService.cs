using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class VersionService
    {
        private readonly VersionQuery _query;

        public VersionService(VersionQuery query)
        {
            _query = query;
        }

        public VersionQuery Query { get { return _query; } }
    }
}
