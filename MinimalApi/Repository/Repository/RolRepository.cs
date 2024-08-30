using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;
using MinimalApi.Repository.Interfaces;

namespace MinimalApi.Repository.Repository;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly ApiContext _context;

    public RolRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Rols
            .ToListAsync();
    }

    public override async Task<Rol?> GetByIdAsync(int id)
    {
        return await _context.Rols
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}
