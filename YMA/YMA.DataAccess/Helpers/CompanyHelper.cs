using YMA.Entities.Models;

namespace YMA.DataAccess.Helpers
{
    public class CompanyHelper
    {
        public static List<CompanyModel> GetCompanyListBySearch(List<CompanyModel> companyList, string? searchText)
        {
            List<CompanyModel> companyModelList = new();
            companyList.ForEach(x =>
            {
                if (SearchHelper.IsSearchedText(x.name, searchText ?? ""))
                {
                    companyModelList.Add(x);
                }
            });
            return companyModelList;
        }
    }
}
