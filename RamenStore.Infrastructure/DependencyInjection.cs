using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RamenStore.Application.Abstractions.Data;
using RamenStore.Domain.Entities.Broths;
using RamenStore.Domain.Entities.Orders;
using RamenStore.Domain.Entities.Proteins;
using RamenStore.Infrastructure.Data;
using RamenStore.Infrastructure.Repositories;

namespace RamenStore.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IProteinRepository, ProteinRepository>();
        services.AddTransient<IBrothRepository, BrothRepository>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<ISqlConnectionFactory>(new SqlConnectionFactory(configuration.GetConnectionString("DefaultConnection")));
        services.AddHttpClient();

        return services;
    }
}