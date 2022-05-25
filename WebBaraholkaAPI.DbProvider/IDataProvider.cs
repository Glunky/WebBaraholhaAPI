using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public interface IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }
    public DbSet<DbFoodCategory> FoodCategories { get; set; }

    public Task SaveAsync();
}