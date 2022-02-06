using Application.Models;
using Infrastructure.Database;

namespace Infrastructure;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<DbContext>();
        services.AddTransient<IRepository<int, Ingredient>, IngredientRepository>();
        return services;
    }
}