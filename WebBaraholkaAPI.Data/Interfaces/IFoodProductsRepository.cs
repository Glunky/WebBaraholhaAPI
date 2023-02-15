using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Data.Interfaces;

public interface IFoodProductsRepository
{
    public Task<List<DbFoodProduct>?> GetFoodProducts(string[] foodProductNames);
    public Task<List<DbFoodProductCategory>?> GetFoodCategories(int[] foodProductCategories);
    public Task<Guid> AddConsumedFoodProductsRecordByUser(List<ConsumedFoodProduct> consumedFoodProducts, string userId);
    public Task<List<DbConsumedFoodProduct>> GetConsumedFoodProductsDuringTime(string userId, DateTime dateFrom, DateTime dateTo, int[] foodProductsCategories, string[] foodProductsNames);
}