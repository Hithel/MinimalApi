using MinimalApi.Dtos.User;
using MinimalApi.Models;
using MinimalApi.ViewModels.User;

namespace MinimalApi.Services.IServices;

public interface IUserService : IGenericService<UserVm, UserDto>
{
    Task<UserVm> GetByUsernameAsync(string username);
    Task<UserVm> GetByRefreshTokenAsync(string username);
}
