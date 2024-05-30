namespace RamenStore.Application.Queries.Proteins.GetAllProteins;

public sealed class ProteinResponse
{
    public string Id { get; init; }
    public string ImageInactive { get; init; }
    public string ImageActive { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}
