namespace RamenStore.Domain.Entities.Orders;

public interface IOrderRepository
{
    Task<Order> PlaceOrderAsync(Order order, CancellationToken cancellationToken = default);
}
