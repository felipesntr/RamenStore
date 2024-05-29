using RamenStore.Domain.Entities.Broths;

namespace RamenStore.Domain.Agregates.Orders;

public interface IBrothRepository
{
    Task<IEnumerable<Broth>> GetAll(CancellationToken cancellationToken = default);
}
