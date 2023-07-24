namespace YMA.Entities.Models
{
    public class CompanyModel
    {
        public string? id { get; set; }

        public string? name { get; set; }

        public string? image_url { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public string? web { get; set; }

        public string? address { get; set; }

        public string? theme_color { get; set; }

        public List<CompanyInviteModel>? company_invite_list { get; set; }
    }
}
