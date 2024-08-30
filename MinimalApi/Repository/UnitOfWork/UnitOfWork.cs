using MinimalApi.Data;
using MinimalApi.Repository.Interfaces;
using MinimalApi.Repository.Repository;

namespace MinimalApi.Repository.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{

    private readonly ApiContext _context;
    public UnitOfWork(ApiContext context)
    {
        _context = context;
    }


    private UserRepository? _users;

    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    private RolRepository? _roles;
    public IRol Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }





}
