using RamenStore.Application.Abstractions.Messaging;

namespace RamenStore.Application.Queries.Proteins.GetAllProteins;

public sealed record GetAllProteinsQuery : IQuery<GetAllProteinsQueryResponse>;