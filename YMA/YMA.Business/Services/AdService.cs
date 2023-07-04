using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class AdService
    {
        private readonly AdQuery _query;

        public AdService(AdQuery query)
        {
            _query = query;
        }

        public AdQuery Query { get { return _query; } }
    }
}
