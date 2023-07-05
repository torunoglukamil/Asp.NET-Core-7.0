using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class brand
{
    public int id { get; set; }

    public string? name { get; set; }

    public string? image_url { get; set; }

    public bool? is_disabled { get; set; }
}
