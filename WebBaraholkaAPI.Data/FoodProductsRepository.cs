using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data;

public class FoodProductsRepository : IFoodProductsRepository
{
    private readonly IDataProvider _provider;

    public FoodProductsRepository([FromServices] IDataProvider provider)
    {
        _provider = provider;
    }

    public async Task AddFoodProducts(List<DbFoodProduct> foodProducts)
    {
        foreach (var foodProduct in foodProducts)
        {
            _provider.FoodProducts.Add(foodProduct);
        }
        
        await _provider.SaveAsync();
    }

    public async Task<List<DbFoodProduct>> GetFoodProducts(List<Guid> ids)
    {
        List<DbFoodProduct> result = new();

        foreach (var id in ids)
        {
            DbFoodProduct dbFoodProduct = await _provider.FoodProducts.FindAsync(id);

            if (dbFoodProduct == null)
            {
                return null;
            }
            
            result.Add(dbFoodProduct);
        }
        
        return result;
    }
}