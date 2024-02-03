using AutoMapper;
using N5Now.Api.Entities;
using N5Now.Api.ViewModel.DTO;

namespace N5Now.Api.Repository
{
    public class AutomapperPermissionProfile : Profile
    {
        public AutomapperPermissionProfile()
        {
            CreateMap<PermissionDTO, Permission>();
            CreateMap<Permission, PermissionDTO>();
        }
    }
}
