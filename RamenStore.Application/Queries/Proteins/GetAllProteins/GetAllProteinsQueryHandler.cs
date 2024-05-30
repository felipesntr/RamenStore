using Dapper;
using RamenStore.Application.Abstractions.Data;
using RamenStore.Application.Abstractions.Messaging;
using RamenStore.Domain.Abstractions;

namespace RamenStore.Application.Queries.Proteins.GetAllProteins;

internal sealed class GetAllProteinsQueryHandler : IQueryHandler<GetAllProteinsQuery, GetAllProteinsQueryResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAllProteinsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<GetAllProteinsQueryResponse>> Handle(GetAllProteinsQuery request, CancellationToken cancellationToken)
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
            FROM Proteins
            """;

        var Proteins = await connection.QueryAsync<ProteinResponse>(sql);

        return Result<IEnumerable<ProteinResponse>>.Success(new GetAllProteinsQueryResponse { Data = Proteins.ToList() });
    }
}
