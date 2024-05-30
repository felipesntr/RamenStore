using Microsoft.EntityFrameworkCore;
using RamenStore.Domain.Entities.Broths;

namespace RamenStore.Infrastructure.Repositories;

public class BrothRepository : IBrothRepository
{
    private readonly ApplicationDbContext _context;

    public BrothRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Broth>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Broths.ToListAsync(cancellationToken);
    }

    public async Task<Broth> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Broths.SingleAsync(b => b.Id == id, cancellationToken);
    }
}