using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Agregates.Orders;

public sealed class Order : Entity
{
    public Order(Guid id, string brothId, string proteinId, string description, string image) : base(id)
    {
        BrothId = brothId;
        ProteinId = proteinId;
        Description = description;
        Image = image;
    }

    public string BrothId { get; set; }
    public string ProteinId { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
}
