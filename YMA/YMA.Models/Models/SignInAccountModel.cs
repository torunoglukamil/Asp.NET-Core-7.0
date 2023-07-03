namespace YMA.Entities.Models
{
    public class SignInAccountModel
    {
        private string? _email;
        private string? _password;

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
    }
}
