namespace YMA.DataAccess.Helpers
{
    public class SearchHelper
    {
        public static bool IsSearchedText(string? text, string searchText) => (text ?? "").Replace(" ", "").ToLower().Contains(searchText.Replace(" ", "").ToLower());

        public static bool IsSearchedText(List<string?> textList, string searchText)
        {
            bool isSearchedText = false;
            foreach (string? text in textList)
            {
                if (IsSearchedText(text, searchText))
                {
                    isSearchedText = true;
                    break;
                }
            }
            return isSearchedText;
        }
    }
}
