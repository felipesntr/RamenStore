using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Agregates.Orders;

public static class BrothErrors
{
    public static readonly Error NotFound = new(
        "Broth.NotFound",
        "The broth with the specified identifier was not found");
}
