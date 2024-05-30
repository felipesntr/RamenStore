using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Entities.Orders;

public static class OrderErrors
{
    public static readonly Error NotFound = new(
        "Order.NotFound",
        "The order with the specified identifier was not found");

    public static readonly Error CouldNot = new(
        "Order.CouldNot",
        "could not place order");

    public static readonly Error BothParameters = new(
        "Order.BothParameters",
        "both brothId and proteinId are required");
}
