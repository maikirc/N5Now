namespace N5Now.Api.Entities;

public partial class TypePermission
{
    public int IdTypePermission { get; set; }

    public string Description { get; set; } = null!;

    public bool State { get; set; }

    public DateTime CreationDate { get; set; }

    public string CreationUser { get; set; } = null!;

    public DateTime LastModificationDate { get; set; }

    public string LastModificationUser { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
