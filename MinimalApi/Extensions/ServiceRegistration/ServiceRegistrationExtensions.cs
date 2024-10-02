

using Microsoft.AspNetCore.Identity;
using MinimalApi.Helpers.Authentication.Security;
using MinimalApi.Helpers.Services;
using MinimalApi.Helpers.TwoStepAuth;
using MinimalApi.Models.Entities;
using MinimalApi.Services.Implementations;
using MinimalApi.Services.IServices;

namespace MinimalApi.Extensions.ServiceRegistration;

public static class ServiceRegistrationExtensions
{
    public static void AddServices(this IServiceCollection service)
    {
        //Servicio de entidades
        service.AddScoped<IRolService, RolService>();
        service.AddScoped<IUserService, UserService>();


        //Servicios de Authorization
        service.AddScoped<IUserAuthenticationService, UserAuthenticationService>();


        //servicios de Two Step Authentication
        service.AddScoped<IAuth, Auth>();

        //Servicio de hasheo de contrasena
        service.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        //Servicio de generacion de token
        service.AddScoped<ITokenService, TokenService>();

    }
}
