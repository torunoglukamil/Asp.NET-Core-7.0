namespace YMA.Entities.Models
{
    public class CompanyInviteModel
    {
        public int id { get; set; }

        public int? receiver_id { get; set; }

        public int? sender_id { get; set; }

        public bool? is_buying { get; set; }

        public bool? is_selling { get; set; }

        public bool? is_current_account_registration { get; set; }

        public bool? is_accepted { get; set; }
    }
}
