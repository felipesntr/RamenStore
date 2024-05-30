namespace RamenStore.Application.Commands.Orders.PlaceAnOrder;

public class PlaceAnOrderCommandResponse
{
    public PlaceAnOrderCommandResponse(string id, string description, string image)
    {
        Id = id;
        Description = description;
        Image = image;
    }

    public string Id { get; init; }
    public string Description { get; init; }
    public string Image { get; init; }
}