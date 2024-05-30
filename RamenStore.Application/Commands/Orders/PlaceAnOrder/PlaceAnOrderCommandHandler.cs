using RamenStore.Application.Abstractions.Messaging;
using RamenStore.Domain.Abstractions;
using RamenStore.Domain.Entities.Broths;
using RamenStore.Domain.Entities.Orders;
using RamenStore.Domain.Entities.Proteins;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RamenStore.Application.Commands.Orders.PlaceAnOrder;

internal sealed class PlaceAnOrderCommandHandler : ICommandHandler<PlaceAnOrderCommand, PlaceAnOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProteinRepository _proteinRepository;
    private readonly IBrothRepository _brothRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public PlaceAnOrderCommandHandler(IOrderRepository orderRepository, IHttpClientFactory httpClientFactory, IProteinRepository proteinRepository, IBrothRepository brothRepository)
    {
        _orderRepository = orderRepository;
        _httpClientFactory = httpClientFactory;
        _proteinRepository = proteinRepository;
        _brothRepository = brothRepository;
    }

    public async Task<Result<PlaceAnOrderCommandResponse>> Handle(PlaceAnOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.ProteinId)
                || string.IsNullOrWhiteSpace(request.BrothId))
            {
                return Result.Failure<PlaceAnOrderCommandResponse>(OrderErrors.BothParameters);
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", "ZtVdh8XQ2U8pWI2gmZ7f796Vh8GllXoN7mr0djNf");

            var response = await client.PostAsync("https://api.tech.redventures.com.br/orders/generate-id", null);

            if (!response.IsSuccessStatusCode)
                throw new InvalidOperationException("Failed to generate order ID.");

            var content = await response.Content.ReadAsStringAsync();
            var generateIdResponse = JsonSerializer.Deserialize<GenerateIdHttpResponse>(content);

            if (generateIdResponse == null)
                throw new InvalidOperationException("Failed to parse order ID response.");

            Protein protein = await _proteinRepository.GetByIdAsync(request.ProteinId, cancellationToken);
            Broth broth = await _brothRepository.GetByIdAsync(request.BrothId, cancellationToken);

            var orderId = generateIdResponse.OrderId;
            var description = $"{broth.Name} and {protein.Name} Ramen";

            return Result.Success(new PlaceAnOrderCommandResponse(orderId, description, ""));
        }
        catch (Exception)
        {
            return Result.Failure<PlaceAnOrderCommandResponse>(OrderErrors.CouldNot);
        }
    }
}

internal record GenerateIdHttpResponse
{
    [JsonPropertyName("orderId")]
    public string OrderId { get; set; } = string.Empty;
};

