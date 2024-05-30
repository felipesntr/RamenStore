using RamenStore.Application.Abstractions.Messaging;

namespace RamenStore.Application.Queries.Broths.GetAllBroths;

public sealed record GetAllBrothsQuery : IQuery<GetAllBrothsQueryResponse>;