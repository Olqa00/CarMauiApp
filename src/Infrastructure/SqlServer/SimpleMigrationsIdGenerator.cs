namespace CarMauiApp.Infrastructure.SqlServer;

using Microsoft.EntityFrameworkCore.Migrations;

public sealed class SimpleMigrationsIdGenerator : IMigrationsIdGenerator
{
    public string GenerateId(string name) => name;

    public string GetName(string id) => id;

    public bool IsValidId(string id) => true;
}
