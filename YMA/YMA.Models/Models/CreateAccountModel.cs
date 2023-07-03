namespace YMA.Entities.Models
{
    public class CreateAccountModel
    {
        public string? email { get; set; }

        public string? password { get; set; }

        public string? password_again { get; set; }
    }
}
