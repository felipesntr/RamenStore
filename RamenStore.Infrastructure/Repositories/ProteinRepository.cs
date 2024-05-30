using Microsoft.EntityFrameworkCore;
using RamenStore.Domain.Entities.Proteins;

namespace RamenStore.Infrastructure.Repositories;

public class ProteinRepository : IProteinRepository
{
    private readonly ApplicationDbContext _context;

    public ProteinRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Protein>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Proteins.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<Protein> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _context.Proteins.SingleAsync(b => b.Id == id, cancellationToken);
    }
}
