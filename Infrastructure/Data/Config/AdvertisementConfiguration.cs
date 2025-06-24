using Domain.AggregateModels.AdvertisementAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class AdvertisementConfiguration:IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.Property(static advertisement => advertisement.Name).HasMaxLength(AdvertisementConstants.MaxNameLength);
        builder.Property(static advertisement => advertisement.Description).HasMaxLength(AdvertisementConstants.MaxDescriptionLength);
        builder.HasOne(static advertisement => advertisement.Category).WithMany(static advertisementCategory => advertisementCategory.Advertisements);
    }
}