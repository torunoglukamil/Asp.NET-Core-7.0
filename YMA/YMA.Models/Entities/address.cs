using System;
using System.Collections.Generic;

namespace YMA.Models.Entities;

public partial class address
{
    public int id { get; set; }

    public string? title { get; set; }

    public string? full_address { get; set; }

    public string? province { get; set; }

    public string? district { get; set; }

    public string? neighbourhood { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }

    public virtual ICollection<account> accounts { get; set; } = new List<account>();
}
