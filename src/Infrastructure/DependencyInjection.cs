namespace CarMauiApp.Infrastructure;

using CarMauiApp.Application.Interfaces;
using CarMauiApp.Infrastructure.Services;
using CarMauiApp.Infrastructure.SqlServer;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSqlServer(configuration);
        services.AddScoped<ICarService, CarService>();

        return services;
    }
}
