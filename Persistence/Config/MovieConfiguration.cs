using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasQueryFilter(m => m.IsActive);

        builder.Property(m => m.Title).IsRequired();

        builder.Property(m => m.Genre).IsRequired();

        builder.Property(m => m.Description).IsRequired();

        builder.Property(m => m.Director).IsRequired();

        builder.Property(m => m.Duration).IsRequired();

        builder.Property(m => m.Rating).HasColumnType("decimal(18,2)");
    }
}
