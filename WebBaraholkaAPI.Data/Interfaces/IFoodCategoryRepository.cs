using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data.Interfaces;

public interface IFoodCategoryRepository
{
    public Task<List<DbFoodCategory>?> GetFoodCategories(List<int> foodCategoriesIds);
}