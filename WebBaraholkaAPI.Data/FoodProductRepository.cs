using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data;

public class FoodProductRepository : IFoodProductRepository
{
    private readonly IDataProvider _provider;

    public FoodProductRepository([FromServices] IDataProvider provider)
    {
        _provider = provider;
    }


    public bool AddFoodProduct(Guid id)
    {
        throw new NotImplementedException();
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