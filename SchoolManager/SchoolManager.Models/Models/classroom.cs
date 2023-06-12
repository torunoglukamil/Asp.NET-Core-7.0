using System;
using System.Collections.Generic;

namespace SchoolManager.Models.Models;

public partial class classroom
{
    public int id { get; set; }

    public short grade { get; set; }

    public string branch { get; set; } = null!;

    public virtual ICollection<student>? students { get; set; }
}
