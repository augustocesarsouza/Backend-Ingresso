using Ingresso.Api.Authentication;
using Ingresso.Api.Controllers;
using Ingresso.Api.ControllersInterface;
using Ingresso.Application.MyHubs;
using Ingresso.Domain.Authentication;
using Ingresso.Infra.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Ingresso.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentUser, CurrentUser>();
            builder.Services.AddScoped<IBaseController, BaseController>();

            builder.Services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolity", builder =>
                {
                    builder.WithOrigins("http://localhost:6400")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("Secret") ?? "GXN0jYSyPZYP3D3WULO8naEHQp9XRP347UvK5I"));

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            builder.Services.AddEndpointsApiExplorer(); // Principalmente para Swagger
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddSignalR();

            var app = builder.Build();
            app.UseRouting();

            app.UseCors("CorsPolity");

            app.MapControllers();
            app.UseHttpsRedirection();

            //app.UseHangfireDashboard(); Criar jobs
            //app.MapHangfireDashboard();
            //RecurringJob.AddOrUpdate<IUserService>(x => x.GetUsers(), "0 * * ? * *");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GeneralHub>("/generalhub");
            });

            app.Run();
        }
    }
}
