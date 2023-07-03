namespace YMA.Entities.Models
{
    public class EmailModel
    {
        private string? _email;

        public string? email
        {
            get { return _email; }
            set { _email = value!.Replace(" ", "").ToLower(); }
        }
    }
}
