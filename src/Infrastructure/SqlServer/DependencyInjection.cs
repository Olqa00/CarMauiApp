namespace CarMauiApp.Infrastructure.SqlServer;

public static class DependencyInjection
{
    private const string OPTIONS_SECTION_NAME = "SqlServer";

    public static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        if (!configuration.GetSection(OPTIONS_SECTION_NAME).Exists())
        {
            throw new Exception($"Section '{OPTIONS_SECTION_NAME}' not found in configuration!");
        }

        var section = configuration.GetSection(OPTIONS_SECTION_NAME);
        services.Configure<SqlServerOptions>(section);
        var options = configuration.GetOptions<SqlServerOptions>(OPTIONS_SECTION_NAME);

        options.InitializeStaticConnection();

        services.AddDbContext<CarMauiDbContext>(option => option.UseSqlServer(options.ConnectionString,
            b => b.MigrationsAssembly(typeof(CarMauiDbContext).Assembly.FullName)));

        return services;
    }

    private static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}
