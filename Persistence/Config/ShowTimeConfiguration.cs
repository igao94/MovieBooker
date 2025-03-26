using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ShowtimeConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.HasQueryFilter(st => st.Movie.IsActive);

        builder.HasQueryFilter(st => st.StartTime > DateTime.UtcNow);

        builder.HasOne(st => st.Movie)
            .WithMany(m => m.Showtimes)
            .HasForeignKey(m => m.MovieId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
