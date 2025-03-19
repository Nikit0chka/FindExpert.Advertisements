using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.ServiceOfferRequestCategoryAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal sealed class Context:DbContext
{
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }

    public Context(DbContextOptions<Context> options):base(options) { Database.EnsureCreated(); }
}