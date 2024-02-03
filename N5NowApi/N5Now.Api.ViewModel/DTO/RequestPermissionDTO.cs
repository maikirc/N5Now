using System;

namespace N5Now.Api.ViewModel.DTO
{
    public class RequestPermissionDTO
    {
        public string Id { get; set; }

        public string NameOperation { get; set; } = null!;

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
    }
}
