using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class version
{
    public string id { get; set; } = null!;

    public string? name { get; set; }

    public string? version1 { get; set; }

    public string? download_url { get; set; }
}
