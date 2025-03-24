using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ShowTimeConfiguration : IEntityTypeConfiguration<ShowTime>
{
    public void Configure(EntityTypeBuilder<ShowTime> builder)
    {
        builder.HasQueryFilter(st => st.Movie.IsActive);

        builder.HasQueryFilter(st => st.StartTime > DateTime.UtcNow);

        builder.HasOne(st => st.Movie)
            .WithMany(m => m.Shows)
            .HasForeignKey(m => m.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
