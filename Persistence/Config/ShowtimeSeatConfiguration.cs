using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ShowtimeSeatConfiguration : IEntityTypeConfiguration<ShowtimeSeat>
{
    public void Configure(EntityTypeBuilder<ShowtimeSeat> builder)
    {
        builder.HasQueryFilter(ss => ss.Showtime.Movie.IsActive);

        builder.Property(ss => ss.Price).HasColumnType("decimal(18,2)");

        builder.Property(ss => ss.Currency).IsRequired();

        builder.HasOne(ss => ss.Showtime)
            .WithMany(st => st.ShowtimeSeats)
            .HasForeignKey(ss => ss.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
