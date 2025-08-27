namespace CarMauiApp.Infrastructure.SqlServer.Configuration;

using CarMauiApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CarConfiguration : IEntityTypeConfiguration<CarEntity>

{
    public void Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.HasKey(treatment => treatment.Id);
    }
}
