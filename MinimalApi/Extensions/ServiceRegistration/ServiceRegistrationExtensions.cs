

using MinimalApi.Helpers.Services;
using MinimalApi.Helpers.TwoStepAuth;
using MinimalApi.Services.Implementations;
using MinimalApi.Services.IServices;

namespace MinimalApi.Extensions.ServiceRegistration;

public static class ServiceRegistrationExtensions
{
    public static void AddServices (this IServiceCollection service)
    {
        //Servicio de entidades
        service.AddScoped<IRolService, RolService>();
        service.AddScoped<IUserService, UserService>();


        //Servicios de Authorization
        service.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        

        //servicios de Two Step Authentication
        service.AddScoped<IAuth, Auth>();
    }
}
