using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data.Implementations;

public class FoodProductsRepository : IFoodProductsRepository
{
    private readonly IDataProvider _provider;

    public FoodProductsRepository([FromServices] IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task<List<DbFoodProduct>?> GetFoodProducts(List<string> productNames)
    {
        List<DbFoodProduct> result = new();

        foreach (var productName in productNames)
        {
            DbFoodProduct? dbFoodProduct =  await _provider.FoodProducts
                .FirstOrDefaultAsync(fp => fp.Name == productName);

            if (dbFoodProduct == null)
            {
                return null;
            }
            
            result.Add(dbFoodProduct);
        }
        
        return result;
    }
}