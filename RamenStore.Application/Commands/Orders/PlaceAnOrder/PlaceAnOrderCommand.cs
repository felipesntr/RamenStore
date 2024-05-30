using RamenStore.Application.Abstractions.Messaging;

namespace RamenStore.Application.Commands.Orders.PlaceAnOrder;

public sealed record PlaceAnOrderCommand(string BrothId, string ProteinId) : ICommand<PlaceAnOrderCommandResponse>;
