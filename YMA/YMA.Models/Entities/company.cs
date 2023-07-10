using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class company
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? image_url { get; set; }

    public string? email { get; set; }

    public string? phone { get; set; }

    public string? web { get; set; }

    public string? address { get; set; }

    public string? theme_color { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }

    public virtual ICollection<company_invite> company_invitereceivers { get; set; } = new List<company_invite>();

    public virtual ICollection<company_invite> company_invitesenders { get; set; } = new List<company_invite>();

    public virtual ICollection<featured_company> featured_companies { get; set; } = new List<featured_company>();

    public virtual ICollection<product> products { get; set; } = new List<product>();
}
