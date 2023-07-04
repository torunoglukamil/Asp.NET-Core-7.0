using YMA.DataAccess.Queries;

namespace YMA.Business.Services
{
    public class CategoryService
    {
        private readonly CategoryQuery _query;

        public CategoryService(CategoryQuery query)
        {
            _query = query;
        }

        public CategoryQuery Query { get { return _query; } }
    }
}
