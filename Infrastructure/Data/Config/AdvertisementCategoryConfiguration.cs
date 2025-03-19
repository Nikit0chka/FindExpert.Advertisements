using Domain.AggregateModels.ServiceOfferRequestCategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class AdvertisementCategoryConfiguration:IEntityTypeConfiguration<AdvertisementCategory>
{
    public void Configure(EntityTypeBuilder<AdvertisementCategory> builder)
    {
        builder.Property(static advertisementCategory => advertisementCategory.Name).HasMaxLength(AdvertisementCategoryConstants.MaxNameLength);

        builder.HasMany(static advertisementCategory => advertisementCategory.Advertisements).WithOne(static advertisement => advertisement.AdvertisementCategory).HasForeignKey(static advertisement => advertisement.AdvertisementCategoryId).IsRequired();

        builder.HasMany(static advertisementCategory => advertisementCategory.ChildCategories).WithOne(static advertisementCategory => advertisementCategory.ParentCategory).HasForeignKey(static advertisementCategory => advertisementCategory.ParentCategoryId)
            .IsRequired(false);
    }
}