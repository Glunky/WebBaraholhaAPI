using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Data.Interfaces;

public interface IFoodProductsRepository
{
    public Task<List<DbFoodProduct>?> GetFoodProducts(List<string> names);
    public Task<List<DbFoodCategory>?> GetFoodCategories(List<int> foodCategoriesIds);
    public Task<Guid> AddConsumedFoodRecordByUser(List<ConsumedFoodProduct> consumedFoodProducts, string userId);
    public Task<List<DbConsumedFoodProductRecord>> GetRecordsForUserFromTo(string userId, DateTime from, DateTime to);
}