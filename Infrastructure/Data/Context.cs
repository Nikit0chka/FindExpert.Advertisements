using Domain.AggregateModels.AdvertisementAggregate;
using Domain.AggregateModels.CategoryAggregate;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

internal sealed class Context:DbContext
{
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<Category> AdvertisementCategories { get; set; }

    public Context(DbContextOptions<Context> options):base(options) { Database.EnsureCreated(); }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AdvertisementCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());

        InitializeBaseData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void InitializeBaseData(ModelBuilder modelBuilder)
    {
        var advertisementCategory1 = new Category("Бытовая техника", null)
                                     {
                                         Id = 1
                                     };

        var advertisementCategory2 = new Category("Электроника", null)
                                     {
                                         Id = 2
                                     };

        var advertisementCategory3 = new Category("Квартиры и дома", null)
                                     {
                                         Id = 3
                                     };

#region Бытовая техника

        var advertisementCategory4 = new Category("Стиральные машины", 1)
                                     {
                                         Id = 4
                                     };

        var advertisementCategory5 = new Category("Холодильники", 1)
                                     {
                                         Id = 5
                                     };

        var advertisementCategory6 = new Category("Посудомоечные машины", 1)
                                     {
                                         Id = 6
                                     };

        var advertisementCategory7 = new Category("Прочее", 1)
                                     {
                                         Id = 7
                                     };

#endregion

#region Электроника

        var advertisementCategory8 = new Category("Компьютеры", 2)
                                     {
                                         Id = 8
                                     };

        var advertisementCategory9 = new Category("Телефоны", 2)
                                     {
                                         Id = 9
                                     };

        var advertisementCategory10 = new Category("Телевизоры", 2)
                                      {
                                          Id = 10
                                      };

        var advertisementCategory11 = new Category("Прочее", 2)
                                      {
                                          Id = 11
                                      };

#endregion

#region Квартиры и дома

        var advertisementCategory12 = new Category("Поклейка обоев", 3)
                                      {
                                          Id = 12
                                      };

        var advertisementCategory13 = new Category("Электромонтаж", 3)
                                      {
                                          Id = 13
                                      };

        var advertisementCategory14 = new Category("Штукатурка", 3)
                                      {
                                          Id = 14
                                      };

        var advertisementCategory15 = new Category("Прочее", 3)
                                      {
                                          Id = 15
                                      };

#endregion


        modelBuilder.Entity<Category>().HasData(new List<Category>
                                                             {
                                                                 advertisementCategory1, advertisementCategory2, advertisementCategory3, advertisementCategory4, advertisementCategory5,
                                                                 advertisementCategory6, advertisementCategory7, advertisementCategory8, advertisementCategory9, advertisementCategory10,
                                                                 advertisementCategory11, advertisementCategory12, advertisementCategory13, advertisementCategory14, advertisementCategory15
                                                             });
    }
}