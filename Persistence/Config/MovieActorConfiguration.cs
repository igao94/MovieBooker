using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
{
    public void Configure(EntityTypeBuilder<MovieActor> builder)
    {
        builder.HasQueryFilter(ma => ma.Movie.IsActive);

        builder.Ignore(ma => ma.Id);

        builder.HasKey(ma => new { ma.MovieId, ma.ActorId });

        builder.HasOne(ma => ma.Movie)
            .WithMany(m => m.Actors)
            .HasForeignKey(ma => ma.MovieId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ma => ma.Actor)
            .WithMany(a => a.Movies)
            .HasForeignKey(ma => ma.ActorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
