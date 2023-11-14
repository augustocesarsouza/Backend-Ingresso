using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Domain.Entities;

namespace Ingresso.Application.Mappings
{
    internal class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<UserDto, User>();
            CreateMap<PermissionDTO, Permission>();
            CreateMap<UserPermissionDTO, UserPermission>(); 
            CreateMap<AdditionalInfoUserDTO, AdditionalInfoUser>();
        }
    }
}
