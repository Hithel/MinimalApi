using MinimalApi.Dtos.Rol;
using MinimalApi.Repository.Interfaces;
using MinimalApi.ViewModels.Rol;
using System.Linq.Expressions;

namespace MinimalApi.Services.IServices;

public interface IRolService : IGenericService<RolVm, RolDto>
{

}
