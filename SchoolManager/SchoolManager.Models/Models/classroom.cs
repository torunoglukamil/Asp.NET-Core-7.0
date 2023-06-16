namespace SchoolManager.Models.Models;

public partial class Classroom
{
    private string? _branch;

    public int? id { get; set; }

    public int? grade { get; set; }

    public string? branch
    {
        get { return _branch; }
        set { _branch = value!.Trim().ToUpper(); }
    }

    public virtual ICollection<Student>? students { get; set; }
}
