namespace RamenStore.Domain.Entities.Broths;

public interface IBrothRepository
{
    Task<IEnumerable<Broth>> GetAll(CancellationToken cancellationToken = default);
}
