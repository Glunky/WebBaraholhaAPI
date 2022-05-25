using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data.Implementations;

public class FoodCategoryRepository : IFoodCategoryRepository
{
    private readonly IDataProvider _provider;

    public FoodCategoryRepository([FromServices] IDataProvider provider)
    {
        _provider = provider;
    }
    
    public async Task<List<DbFoodCategory>?> GetFoodCategories(List<int> foodCategoriesIds)
    {
        List<DbFoodCategory> result = new();
        foreach (var foodCategoryId in foodCategoriesIds)
        {
            DbFoodCategory? dbFoodCategory = await _provider.FoodCategories
                .Include(fc => fc.FoodProducts)
                .FirstOrDefaultAsync(fc => fc.Id == foodCategoryId);

            if (dbFoodCategory == null)
            {
                return null;
            }

            result.Add(dbFoodCategory);
        }

        return result;
    }
}