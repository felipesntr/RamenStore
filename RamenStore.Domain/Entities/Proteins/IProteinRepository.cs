using RamenStore.Domain.Entities.Broths;

namespace RamenStore.Domain.Entities.Proteins;

public interface IProteinRepository
{
    Task<IEnumerable<Protein>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Protein> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
