namespace RamenStore.Domain.Abstractions;

public abstract class AgregateRoot
{
    protected AgregateRoot(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
