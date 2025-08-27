namespace CarMauiApp.Infrastructure.SqlServer;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

public sealed class CarMauiDbContextFactory : IDesignTimeDbContextFactory<CarMauiDbContext>
{
    private const string FILE_PATH = @"../Maui";
    private const string OPTIONS_CONNECTION_STRING = "ConnectionString";
    private const string OPTIONS_SECTION_NAME = "SqlServer";

    public CarMauiDbContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), FILE_PATH));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetSection(OPTIONS_SECTION_NAME)[OPTIONS_CONNECTION_STRING];

        var optionsBuilder = new DbContextOptionsBuilder<CarMauiDbContext>();

        optionsBuilder
            .UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(CarMauiDbContext).Assembly.FullName))
            .ReplaceService<IMigrationsIdGenerator, SimpleMigrationsIdGenerator>();

        return new CarMauiDbContext(optionsBuilder.Options);
    }
}
