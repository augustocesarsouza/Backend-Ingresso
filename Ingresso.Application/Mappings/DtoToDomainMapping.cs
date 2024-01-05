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
            CreateMap<MovieDTO, Movie>();
            CreateMap<MovieTheaterDTO, MovieTheater>();
            CreateMap<TheatreDTO, Theatre>();
            CreateMap<RegionDTO, Region>();
            CreateMap<CinemaDTO, Cinema>();
            CreateMap<CinemaMovieDTO, CinemaMovie>()
                .ForMember(dest => dest.Cinema, opt => opt.MapFrom(src => src.CinemaDTO));
            CreateMap<FormOfPaymentDTO, FormOfPayment>();
            CreateMap<AdditionalFoodMovieDTO, AdditionalFoodMovie>();
        }
    }
}
