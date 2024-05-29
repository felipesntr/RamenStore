using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/broths", (HttpRequest request) =>
{
    if (!request.Headers.TryGetValue("x-api-key", out var apiKey) || string.IsNullOrWhiteSpace(apiKey))
    {
        return Results.Json(new { error = "x-api-key header missing" }, statusCode: 403);
    }

    var broths = new List<string>();

    return Results.Json(broths);
})
.WithName("listBroths")
.Produces(200, typeof(IEnumerable<object>))
.Produces(403, typeof(object));

app.Run();