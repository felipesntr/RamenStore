namespace RamenStore.Domain.Entities.Proteins;

public interface IProteinRepository
{
    Task<IEnumerable<Protein>> GetAll(CancellationToken cancellationToken = default);
}
