using N5Now.Api.ViewModel.DTO;
using System.Threading.Tasks;

namespace N5Now.Api.IRepository
{
    public interface IPermissionRepository
    {
        Task<PermissionDTO> GetPermission(int idPermission);
        Task<int> RequestPermission(PermissionDTO permission);
        Task<bool> ModifyPermission(PermissionDTO permission, string ipAdress);
    }
}
