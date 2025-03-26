using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeat>
{
    public void Configure(EntityTypeBuilder<ShowtimeSeat> builder)
    {
        builder.HasQueryFilter(ss => ss.Showtime.StartTime > DateTime.UtcNow);

        builder.HasOne(ss => ss.Showtime)
            .WithMany(st => st.ShowtimeSeats)
            .HasForeignKey(ss => ss.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ss => new { ss.ShowtimeId, ss.SeatNumber }).IsUnique();
    }
}
