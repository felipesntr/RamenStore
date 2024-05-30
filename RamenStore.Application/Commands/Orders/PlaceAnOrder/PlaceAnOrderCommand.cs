using RamenStore.Application.Abstractions.Messaging;
using System.Text.Json.Serialization;

namespace RamenStore.Application.Commands.Orders.PlaceAnOrder;

public sealed class PlaceAnOrderCommand : ICommand<PlaceAnOrderCommandResponse>
{
    [JsonPropertyName("brothId")]
    public string BrothId { get; set; }
    [JsonPropertyName("proteinId")]
    public string ProteinId { get; set; }
}