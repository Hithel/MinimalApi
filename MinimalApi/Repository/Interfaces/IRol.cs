using MinimalApi.Models.Entities;

namespace MinimalApi.Repository.Interfaces;

public interface IRol: IGenericRepository<Rol>
{
    Task<Rol?> GetRolByName(string name);
}
