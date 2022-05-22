using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider;

public interface IDataProvider
{
    public DbSet<DbFoodProduct> FoodProducts { get; set; }

    public Task SaveAsync();
}