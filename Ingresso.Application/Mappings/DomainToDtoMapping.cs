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
            CreateMap<AdditionalInfoUser, AdditionalInfoUserDTO>();
            CreateMap<Movie, MovieDTO>();
            CreateMap<MovieTheater, MovieTheaterDTO>();
            CreateMap<Theatre, TheatreDTO>();
            CreateMap<Region, RegionDTO>();
            CreateMap<Cinema, CinemaDTO>();
            CreateMap<CinemaMovie, CinemaMovieDTO>()
                .ForMember(dest => dest.CinemaDTO, opt => opt.MapFrom(src => src.Cinema))
                .ForMember(x => x.CinemaId, opt => opt.Ignore())
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.MovieId, opt => opt.Ignore())
                .ForMember(x => x.RegionId, opt => opt.Ignore());
            
        }
    }
}
