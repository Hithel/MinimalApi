using MinimalApi.Dtos.User;
using MinimalApi.ViewModels.User;

namespace MinimalApi.Services.IServices;

public interface IUserService : IGenericService<UserVm, UserDto>
{

}
