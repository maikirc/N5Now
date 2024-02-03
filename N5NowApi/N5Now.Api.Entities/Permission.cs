namespace N5Now.Api.Entities;

public partial class Permission
{
    public int IdPermission { get; set; }

    public int IdEmployee { get; set; }

    public int IdTypePermission { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateUntil { get; set; }

    public string Observation { get; set; } = null!;

    public bool State { get; set; }

    public DateTime CreationDate { get; set; }

    public string CreationUser { get; set; } = null!;

    public DateTime LastModificationDate { get; set; }

    public string LastModificationUser { get; set; } = null!;

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual TypePermission IdTypePermissionNavigation { get; set; } = null!;
}
