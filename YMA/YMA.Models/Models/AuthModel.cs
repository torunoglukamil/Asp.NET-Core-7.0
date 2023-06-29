namespace YMA.Models.Models
{
    public class AuthModel
    {
        public string? email { get; set; }

        public string? password { get; set; }

        public string? password_again { get; set; }

        public bool? is_creation { get; set; }
    }
}
