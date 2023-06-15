namespace SchoolManager.Models.Models;

public partial class Classroom
{
    public int? id { get; set; }

    public int? grade { get; set; }

    public string? branch { get; set; }

    public virtual ICollection<Student>? students { get; set; }
}
