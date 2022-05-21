using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data;

public interface IFoodProductsRepository
{
    public Task AddFoodProducts(List<DbFoodProduct> foodProducts);
    public DbFoodProduct GetFoodProduct(Guid id);
    public IQueryable<DbFoodProduct> GetFoodProducts(List<Guid> ids);

}