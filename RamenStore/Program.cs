using RamenStore.Application;
using RamenStore.Infrastructure;
using Microsoft.OpenApi.Models;
using MediatR;
using RamenStore.Application.Queries.Broths.GetAllBroths;
using RamenStore.Application.Queries.Proteins.GetAllProteins;
using RamenStore.Application.Commands.Orders.PlaceAnOrder;
using Microsoft.AspNetCore.Mvc;
using RamenStore.Domain.Entities.Orders;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "RamenStore API", Version = "v1" });
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key needed to access the endpoints"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Name = "x-api-key",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var apiKey = builder.Configuration["ApiKey"];

app.MapGet("/broths", async (HttpRequest request, IMediator _sender, ILogger<Program> logger, IMemoryCache cache) =>
{
    try
    {
        if (!request.Headers.TryGetValue("x-api-key", out var providedApiKey) || string.IsNullOrWhiteSpace(providedApiKey))
        {
            return Results.Json(new { error = "x-api-key header missing" }, statusCode: 403);
        }

        if (!providedApiKey.Equals(apiKey))
        {
            return Results.Json(new { message = "Forbidden" }, statusCode: 403);
        }

        var cacheKey = "brothsList";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<object> cachedBroths))
        {
            var broths = await _sender.Send(new GetAllBrothsQuery());
            if (broths.IsSuccess)
            {
                cachedBroths = broths.Value.Data;
                cache.Set(cacheKey, cachedBroths, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
                });
            }
            else
            {
                return Results.Json(new { error = "Data retrieval failed" }, statusCode: 500);
            }
        }

        return Results.Json(cachedBroths);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception caught: {Message}, StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        return Results.Json(new { error = "Internal Server Error" }, statusCode: 500);
    }
})
.WithName("listBroths")
.Produces<IEnumerable<object>>(200)
.Produces(403, typeof(object))
.Produces(500, typeof(object));

app.MapGet("/proteins", async (HttpRequest request, IMediator _sender, ILogger<Program> logger, IMemoryCache cache) =>
{
    try
    {
        if (!request.Headers.TryGetValue("x-api-key", out var providedApiKey) || string.IsNullOrWhiteSpace(providedApiKey))
        {
            return Results.Json(new { error = "x-api-key header missing" }, statusCode: 403);
        }

        if (!providedApiKey.Equals(apiKey))
        {
            return Results.Json(new { message = "Forbidden" }, statusCode: 403);
        }

        var cacheKey = "proteinsList";
        if (!cache.TryGetValue(cacheKey, out IEnumerable<object> cachedProteins))
        {
            var proteins = await _sender.Send(new GetAllProteinsQuery());
            if (proteins.IsSuccess)
            {
                cachedProteins = proteins.Value.Data;
                cache.Set(cacheKey, cachedProteins, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3000)
                });
            }
            else
            {
                return Results.Json(new { error = "Data retrieval failed" }, statusCode: 500);
            }
        }

        return Results.Json(cachedProteins);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception caught: {Message}, StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        return Results.Json(new { error = "Internal Server Error" }, statusCode: 500);
    }
})
.WithName("listProteins")
.Produces<IEnumerable<object>>(200)
.Produces(403, typeof(object))
.Produces(500, typeof(object));

app.MapPost("/order", async (HttpRequest request, IMediator mediator, ILogger<Program> logger) =>
{
    try
    {
        if (!request.Headers.TryGetValue("x-api-key", out var providedApiKey) || string.IsNullOrWhiteSpace(providedApiKey))
        {
            return Results.Json(new { error = "x-api-key header missing" }, statusCode: 403);
        }

        if (!providedApiKey.Equals(apiKey))
        {
            return Results.Json(new { message = "Forbidden" }, statusCode: 403);
        }

        using var reader = new StreamReader(request.Body);
        var body = await reader.ReadToEndAsync();

        PlaceAnOrderCommand command;
        try
        {
            command = System.Text.Json.JsonSerializer.Deserialize<PlaceAnOrderCommand>(body);
        }
        catch (System.Text.Json.JsonException)
        {
            return Results.Json(new { error = "Invalid JSON format" }, statusCode: 400);
        }

        if (string.IsNullOrEmpty(command.BrothId) || string.IsNullOrEmpty(command.ProteinId))
        {
            return Results.Json(new { error = "both brothId and proteinId are required" }, statusCode: 400);
        }

        var result = await mediator.Send(command);

        if (result.IsFailure)
        {
            if (result.Error.Equals(OrderErrors.BothParameters))
            {
                return Results.Json(new { error = OrderErrors.BothParameters.Name }, statusCode: 400);
            }
        }

        if (result.IsSuccess)
        {
            return Results.Json(new
            {
                id = result.Value.Id,
                description = result.Value.Description,
                image = "not-found.svg"
            }, statusCode: 201);
        }

        return Results.Json(new { error = OrderErrors.CouldNot.Name }, statusCode: 500);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception caught: {Message}, StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        return Results.Json(new { error = "Internal Server Error" });
    }
})
.WithName("placeOrder")
.Produces<ErrorResponse>()
.Produces(400)
.Produces(403);

app.Run();

record ErrorResponse(string error);
