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

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

app.MapGet("/broths", async (HttpRequest request, IMediator _sender, ILogger<Program> logger) =>
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

        var broths = await _sender.Send(new GetAllBrothsQuery());

        return Results.Json(broths.Value.Data);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception caught: {Message}, StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        return Results.Json(new { error = "Internal Server Error" });
    }
})
.WithName("listBroths")
.Produces(200, typeof(IEnumerable<object>))
.Produces(403, typeof(object));

app.MapGet("/proteins", async (HttpRequest request, IMediator _sender, ILogger<Program> logger) =>
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

        var proteins = await _sender.Send(new GetAllProteinsQuery());

        return Results.Json(proteins.Value.Data);
    }
    catch (Exception ex)
    {
        logger.LogError("Exception caught: {Message}, StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        return Results.Json(new { error = "Internal Server Error" });
    }
})
.WithName("listProteins")
.Produces(200, typeof(IEnumerable<object>))
.Produces(403, typeof(object));

app.MapPost("/order", async (PlaceAnOrderCommand command, HttpRequest request, IMediator mediator, ILogger<Program> logger) =>
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
