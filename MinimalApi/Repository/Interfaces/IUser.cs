using MinimalApi.Models;

namespace MinimalApi.Repository.Interfaces;

public interface IUser : IGenericRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByRefreshTokenAsync(string username);
}
