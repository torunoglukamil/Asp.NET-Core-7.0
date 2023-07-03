namespace YMA.Entities.Models
{
    public class CreateAccountModel
    {
        private string? _email;

        public string? email
        {
            get { return _email; }
            set { _email = value!.Replace(" ", "").ToLower(); }
        }

        public string? password { get; set; }

        public string? password_again { get; set; }
    }
}
