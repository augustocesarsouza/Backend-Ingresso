using Ingresso.Application.DTOs.Validations.Interfaces;
using Ingresso.Application.DTOs.Validations.UserValidator;
using Ingresso.Application.Mappings;
using Ingresso.Application.Services;
using Ingresso.Application.Services.Interfaces;
using Ingresso.Domain.Authentication;
using Ingresso.Domain.Repositories;
using Ingresso.Infra.Data.Authentication;
using Ingresso.Infra.Data.Context;
using Ingresso.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ingresso.Infra.IoC
{
    public static class DependectyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            services.AddScoped<IPasswordHasherWrapper, PasswordHasherWrapper>();
            services.AddScoped<IUserCreateDTOValidator, UserCreateDTOValidator>();
            return services;
        }
    }
}
