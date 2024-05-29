using RamenStore.Domain.Abstractions;

namespace RamenStore.Domain.Entities.Proteins;

public static class ProteinErrors
{
    public static readonly Error NotFound = new(
        "Protein.NotFound",
        "The protein with the specified identifier was not found");
}
