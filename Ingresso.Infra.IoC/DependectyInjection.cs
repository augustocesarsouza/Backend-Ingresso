using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.DTOs.Validations.UserValidator;
using Ingresso.Application.Mappings;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Infra.Data.UtilityExternal;
using Ingresso.Infra.Data.UtilityExternal.Interface;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Authentication;
using Ingresso.Infra.Data.Context;
using Ingresso.Infra.Data.Repositories;
using Ingresso.Infra.Data.SendEmailUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sib_api_v3_sdk.Api;

namespace Ingresso.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];

            services.AddDbContext<ApplicationDbContext>(
                  options => options.UseSqlServer(connectionString)); // Server=ms-sql-server; quando depender dele no Docker-Compose


            services.AddStackExchangeRedisCache(redis =>
            {
                redis.Configuration = "localhost:7001";
            });
            //"redis:6379"

            //services.AddHangfire(config => 
            //    config.UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    .UseSqlServerStorage(connectionString)
            //); JOBS

            //services.AddHangfireServer();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<IAdditionalInfoUserRepository, AdditionalInfoUserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieTheaterRepository, MovieTheaterRepository>();
            services.AddScoped<ITokenGeneratorEmail, TokenGeneratorEmail>();
            services.AddScoped<ITheatreRepository, TheatreRepository>();
            services.AddScoped<IRegionTheatreRepository, RegionTheatreRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<ICinemaRepository, CinemaRepository>();
            services.AddScoped<ICinemaMovieRepository, CinemaMovieRepository>();
            services.AddScoped<ITokenGeneratorCpf, TokenGeneratorCpf>();
            services.AddScoped<ISendEmailUser, SendEmailUser>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IUserConfirmationService, UserConfirmationService>();
            services.AddScoped<IMovieTheaterService, MovieTheaterService>();
            services.AddScoped<ITheatreService, TheatreService>();
            services.AddScoped<IAdditionalInfoUserService, AdditionalInfoUserService>();
            services.AddScoped<IRegionTheatreService, RegionTheatreService>();
            services.AddScoped<ISendEmailBrevo, SendEmailBrevo>();
            services.AddScoped<ITransactionalEmailApiUti, TransactionalEmailApiUti>();
            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();
            services.AddScoped<IPasswordHasherWrapper, PasswordHasherWrapper>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<ICinemaMovieService, CinemaMovieService>();
            services.AddScoped<ICacheRedisUti, CacheRedisUti>();
            services.AddScoped<ICloudinaryUti, ClodinaryUti>();
            services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();
            services.AddScoped<ICinemaDTOValidator, CinemaDTOValidator>();
            services.AddScoped<IAdditionalInfoUserDTOValidator, AdditionalInfoUserDTOValidator>();
            services.AddScoped<ICinemaMovieDTOValidator, CinemaMovieDTOValidator>();
            services.AddScoped<IMovieDTOValidator, MovieDTOValidator>();
            services.AddScoped<ITheatreDTOValidator, TheatreDTOValidator>();
            return services;
        }
    }
}

