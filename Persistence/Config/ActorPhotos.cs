using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Config;

public class ActorPhotos : IEntityTypeConfiguration<ActorPhoto>
{
    public void Configure(EntityTypeBuilder<ActorPhoto> builder)
    {
        builder.Property(ap => ap.PublicId).IsRequired();

        builder.Property(ap => ap.Url).IsRequired();

        builder.HasOne(ap => ap.Actor)
            .WithMany(a => a.Photos)
            .HasForeignKey(ap => ap.ActorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
