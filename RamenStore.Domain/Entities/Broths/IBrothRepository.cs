namespace RamenStore.Domain.Entities.Broths;

public interface IBrothRepository
{
    Task<IEnumerable<Broth>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Broth> GetByIdAsync(string id, CancellationToken cancellationToken = default);
}
