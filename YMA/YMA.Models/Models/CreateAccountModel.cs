namespace YMA.Entities.Models
{
    public class CreateAccountModel
    {
        private string? _email;
        private string? _password;
        private string? _password_again;

        public string? email
        {
            get { return _email; }
            set { _email = value!.Replace(" ", "").ToLower(); }
        }

        public string? password
        {
            get { return _password; }
            set { _password = value!.Replace(" ", ""); }
        }

        public string? password_again
        {
            get { return _password_again; }
            set { _password_again = value!.Replace(" ", ""); }
        }
    }
}
