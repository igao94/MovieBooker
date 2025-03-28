using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ShowtimeSeatReservationConfiguration : IEntityTypeConfiguration<ShowtimeSeatReservation>
{
    public void Configure(EntityTypeBuilder<ShowtimeSeatReservation> builder)
    {
        builder.HasQueryFilter(sr => sr.ShowtimeSeat.Showtime.Movie.IsActive);

        builder.HasOne(sr => sr.ShowtimeSeat)
            .WithMany(ss => ss.Reservations)
            .HasForeignKey(sr => sr.ShowtimeSeatId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sr => sr.User)
            .WithMany(u => u.SeatsReserved)
            .HasForeignKey(sr => sr.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(sr => new { sr.ShowtimeSeatId, sr.ReservedDate });
    }
}
