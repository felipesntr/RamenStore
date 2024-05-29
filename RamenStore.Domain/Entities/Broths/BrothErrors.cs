using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Entities.Broths;

public static class BrothErrors
{
    public static readonly Error NotFound = new(
        "Broth.NotFound",
        "The broth with the specified identifier was not found");
}
