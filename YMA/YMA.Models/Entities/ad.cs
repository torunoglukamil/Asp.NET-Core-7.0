﻿using System;
using System.Collections.Generic;

namespace YMA.Entities.Entities;

public partial class ad
{
    public string? image_url { get; set; }

    public int? order_number { get; set; }

    public bool? is_disabled { get; set; }

    public int id { get; set; }
}