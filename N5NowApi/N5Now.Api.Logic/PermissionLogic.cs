using N5Now.Api.IRepository;
using N5Now.Api.ViewModel.DTO;
using System;
using System.Threading.Tasks;

namespace N5Now.Api.Logic
{
    public class PermissionLogic
    {
        private readonly IPermissionRepository _permissionRepository;
        
        public PermissionLogic(IPermissionRepository permissionRepository)
         => _permissionRepository = permissionRepository ?? throw new ArgumentNullException("permissionRepository");

        public async Task<PermissionDTO> GetPermission(int idPermission)
        => await _permissionRepository.GetPermission(idPermission);

        public async Task<int> RequestPermission(PermissionDTO permission)
        => await _permissionRepository.RequestPermission(permission);

        public async Task<bool> ModifyPermission(PermissionDTO permission, string ipAdress)
        => await _permissionRepository.ModifyPermission(permission, ipAdress);
    }
}
