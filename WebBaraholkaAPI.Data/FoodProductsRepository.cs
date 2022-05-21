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

    public DbFoodProduct GetFoodProduct(Guid id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<DbFoodProduct> GetFoodProducts(List<Guid> ids)
    {
        throw new NotImplementedException();
    }
}