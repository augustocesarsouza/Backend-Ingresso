using AutoMapper;
using Ingresso.Application.DTOs;
using Ingresso.Domain.Entities;

namespace Ingresso.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.PasswordHash, opt => opt.Ignore());
                //.ConstructUsing((model, context) =>
                //{
                //    var dto = new UserDto 
                //    {
                //        //Id = model.Id,
                //        //Name = model.Name,
                //        //Email = model.Email,
                //        PasswordHash = null,
                        
                //    };

                //    return dto;
                //});
            CreateMap<Permission, PermissionDTO>();
            CreateMap<UserPermission, UserPermissionDTO>();
        }
    }
}
