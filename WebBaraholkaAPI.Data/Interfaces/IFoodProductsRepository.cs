using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data.Interfaces;

public interface IFoodProductsRepository
{
    public Task AddFoodProducts(List<DbFoodProduct> foodProducts);
    public Task<List<DbFoodProduct>?> GetFoodProducts(List<string> names);

}