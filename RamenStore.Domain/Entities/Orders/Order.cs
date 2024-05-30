using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Entities.Orders;

public sealed class Order : Entity<string>
{
    public Order(string id, string brothId, string proteinId, string description, string image) : base(id)
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
