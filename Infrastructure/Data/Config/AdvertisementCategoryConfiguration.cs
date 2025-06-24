using Domain.AggregateModels.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

public class AdvertisementCategoryConfiguration:IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(static advertisementCategory => advertisementCategory.Name).HasMaxLength(CategoryConstants.MaxNameLength);

        builder.HasMany(static advertisementCategory => advertisementCategory.Advertisements).WithOne(static advertisement => advertisement.Category)
            .HasForeignKey(static advertisement => advertisement.CategoryId).IsRequired();

        builder.HasMany(static advertisementCategory => advertisementCategory.ChildCategories).WithOne(static advertisementCategory => advertisementCategory.ParentCategory)
            .HasForeignKey(static advertisementCategory => advertisementCategory.ParentCategoryId).IsRequired(false);
    }
}