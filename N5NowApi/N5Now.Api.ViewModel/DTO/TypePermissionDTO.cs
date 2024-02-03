using System;

namespace N5Now.Api.ViewModel.DTO
{
    public class TypePermissionDTO
    {
        public int IdTypePermission { get; set; }

        public string Description { get; set; } = null!;

        public bool State { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreationUser { get; set; } = null!;

        public DateTime LastModificationDate { get; set; }

        public string LastModificationUser { get; set; } = null!;
    }
}
