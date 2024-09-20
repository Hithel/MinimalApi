using MinimalApi.Repository.Interfaces;
using MinimalApi.Repository.Implementations;

namespace MinimalApi.Extensions.RepositoryRegistration
{
    public static class RepositoryRegistrationExtensions
    {
        public static void AddRepositories(this IServiceCollection service)
        {
            service.AddScoped<IRol, RolRepository>();
            service.AddScoped<IUser, UserRepository>();
        }
    }
}
