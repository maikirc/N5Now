namespace N5Now.Api.Entities;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte Age { get; set; }

    public string Company { get; set; } = null!;

    public string Department { get; set; } = null!;

    public bool State { get; set; }

    public DateTime CreationDate { get; set; }

    public string CreationUser { get; set; } = null!;

    public DateTime LastModificationDate { get; set; }

    public string LastModificationUser { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
