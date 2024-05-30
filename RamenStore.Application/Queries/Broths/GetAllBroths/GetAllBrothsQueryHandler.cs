using Dapper;
using RamenStore.Application.Abstractions.Data;
using RamenStore.Application.Abstractions.Messaging;
using RamenStore.Domain.Abstractions;

namespace RamenStore.Application.Queries.Broths.GetAllBroths;

internal sealed class GetAllBrothsQueryHandler : IQueryHandler<GetAllBrothsQuery, GetAllBrothsQueryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllBrothsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<GetAllBrothsQueryResponse>> Handle(GetAllBrothsQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                Id,
                ImageInactive,
                ImageActive,
                Name,
                Description,
                Price
            FROM Broths
            """;

        var broths = await connection.QueryAsync<BrothResponse>(sql);

        return Result<IEnumerable<BrothResponse>>.Success(new GetAllBrothsQueryResponse { Data = broths.ToList() });
    }
}
