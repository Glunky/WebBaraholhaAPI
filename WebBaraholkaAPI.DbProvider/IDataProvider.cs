using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public interface IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }
    public DbSet<DbFoodProductCategory> FoodCategories { get; set; }
    public DbSet<DbConsumedFoodProduct> ConsumedFoodProducts { get; set; }
    public DbSet<DbConsumedFoodProductsRecord> ConsumedFoodProductRecords { get; set; }

    public Task SaveAsync();
}