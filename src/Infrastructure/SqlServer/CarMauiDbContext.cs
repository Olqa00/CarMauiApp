namespace CarMauiApp.Infrastructure.SqlServer;

using CarMauiApp.Domain.Entities;

internal sealed class CarMauiDbContext : DbContext
{
    public DbSet<CarEntity> Cars { get; set; }

    public CarMauiDbContext(DbContextOptions<CarMauiDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
