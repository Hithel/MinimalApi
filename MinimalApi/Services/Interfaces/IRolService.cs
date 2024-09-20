using MinimalApi.Models.Dtos.Rol;
using MinimalApi.Models.ViewModels.Rol;
using MinimalApi.Repository.Interfaces;
using System.Linq.Expressions;

namespace MinimalApi.Services.IServices;

public interface IRolService : IGenericService<RolVm, RolDto>
{
    Task<RolVm> GetRolNameByNameAsync(string name);
}
