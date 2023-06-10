using System;
using System.Collections.Generic;

namespace SchoolManager.Models.Models;

public partial class classrooms
{
    public int id { get; set; }

    public short grade { get; set; }

    public string branch { get; set; } = null!;

    public virtual ICollection<students> students { get; set; } = new List<students>();
}
