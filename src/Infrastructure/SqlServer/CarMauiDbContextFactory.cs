namespace CarMauiApp.Infrastructure.SqlServer;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;

public sealed class CarMauiDbContextFactory : IDesignTimeDbContextFactory<CarMauiDbContext>
{
    public CarMauiDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=(local)\\SQLExpress;Database=CarMaui;Trusted_Connection=True;Encrypt=False;";

        var optionsBuilder = new DbContextOptionsBuilder<CarMauiDbContext>();

        optionsBuilder
            .UseSqlServer(connectionString,
                b => b.MigrationsAssembly(typeof(CarMauiDbContext).Assembly.FullName))
            .ReplaceService<IMigrationsIdGenerator, SimpleMigrationsIdGenerator>();

        return new CarMauiDbContext(optionsBuilder.Options);
    }
}
