#nullable disable

namespace CarMauiApp.Infrastructure.Migrations;

using Microsoft.EntityFrameworkCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Cars");
    }

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Cars",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Make = table.Column<string>("nvarchar(max)", nullable: false),
                Model = table.Column<string>("nvarchar(max)", nullable: false),
                Vin = table.Column<string>("nvarchar(max)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cars", x => x.Id);
            });
    }
}
