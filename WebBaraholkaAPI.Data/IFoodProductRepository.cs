using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.Data;

public interface IFoodProductRepository
{
    public bool AddFoodProduct(Guid id);
    public DbFoodProduct GetFoodProduct(Guid id);
    public IQueryable<DbFoodProduct> GetFoodProducts(List<Guid> ids);

}