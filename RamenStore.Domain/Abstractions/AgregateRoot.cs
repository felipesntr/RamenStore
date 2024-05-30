namespace RamenStore.Domain.Abstractions;

public abstract class AgregateRoot<TAgregateId>
{
    protected AgregateRoot(TAgregateId id)
    {
        Id = id;
    }

    public TAgregateId Id { get; init; }
}
