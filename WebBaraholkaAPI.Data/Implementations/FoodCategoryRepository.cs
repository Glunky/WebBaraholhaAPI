using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data.Implementations;

public class FoodCategoryRepository : IFoodCategoryRepository
{
    public Task<List<DbFoodCategory>?> GetFoodCategories(List<int> foodCategories)
    {
        throw new NotImplementedException();
    }
}