namespace YMA.DataAccess.Helpers
{
    public class SearchHelper
    {
        public static bool IsSearchedText(string? text, string searchText) => (text ?? "").Replace(" ", "").ToLower().Contains(searchText.Replace(" ", "").ToLower());
    }
}
