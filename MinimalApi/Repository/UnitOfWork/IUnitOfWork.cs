using MinimalApi.Repository.Interfaces;

namespace MinimalApi.Repository.UnitOfWork;

public interface IUnitOfWork
{
    IRol Roles {  get; }
    IUser Users { get; }
    Task<int> SaveAsync();
}
