using N5Now.Api.ViewModel;
using N5Now.Api.ViewModel.DTO;
using System.Threading.Tasks;

namespace N5Now.Api.IService
{
    public interface IPermissionService
    {
        Task<RespuestaViewModel<PermissionDTO>> GetPermission(PermissionDTO permission);
        Task<RespuestaViewModel<int>> RequestPermission(PermissionDTO permission, string ipAdress);
        Task<RespuestaViewModel<bool>> ModifyPermission(PermissionDTO permission, string ipAdress);
    }
}
