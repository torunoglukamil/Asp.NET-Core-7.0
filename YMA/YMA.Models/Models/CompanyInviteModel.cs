namespace YMA.Entities.Models
{
    public class CompanyInviteModel
    {
        public string? id { get; set; }

        public string? receiver_id { get; set; }

        public string? sender_id { get; set; }

        public bool? is_buying { get; set; }

        public bool? is_selling { get; set; }

        public bool? is_current_account_registration { get; set; }

        public bool? is_accepted { get; set; }

        public CompanyModel? company { get; set; }
    }
}
