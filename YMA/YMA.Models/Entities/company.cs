using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class company
{
    public string id { get; set; } = null!;

    public string? name { get; set; }

    public string? image_url { get; set; }

    public string? email { get; set; }

    public string? phone { get; set; }

    public string? web { get; set; }

    public string? address { get; set; }

    public string? theme_color { get; set; }

    public DateTime? create_date { get; set; }

    public bool? is_disabled { get; set; }
}
