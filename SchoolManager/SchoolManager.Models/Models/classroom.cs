namespace SchoolManager.Models.Models;

public partial class Classroom
{
    public int id { get; set; }

    public short grade { get; set; }

    public string branch { get; set; } = null!;

    public virtual ICollection<Student>? students { get; set; }
}
