

using MinimalApi.Services.Implementations;
using MinimalApi.Services.IServices;

namespace MinimalApi.Extensions.ServiceRegistration;

public static class ServiceRegistrationExtensions
{
    public static void AddServices (this IServiceCollection service)
    {
        service.AddScoped<IRolService, RolService>();
        service.AddScoped<IUserService, UserService>();
    }
}
