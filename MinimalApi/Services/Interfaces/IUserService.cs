using MinimalApi.Models;
using MinimalApi.Models.Dtos.Authentication;
using MinimalApi.Models.Dtos.User;
using MinimalApi.Models.ViewModels.User;

namespace MinimalApi.Services.IServices;

public interface IUserService : IGenericService<UserVm, UserDto>
{
    Task<UserVm> GetByUsernameAsync(string username);
    Task<UserVm> GetByRefreshTokenAsync(string username);
}
