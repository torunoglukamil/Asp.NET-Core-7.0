﻿using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class brand
{
    public string id { get; set; } = null!;

    public string? name { get; set; }

    public string? image_url { get; set; }

    public bool? is_disabled { get; set; }
}
