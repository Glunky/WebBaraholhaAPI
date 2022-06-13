using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;

public interface IConsumedFoodProductToDbModelMapper
{
    public DbConsumedFoodProduct Map(ConsumedFoodProduct request, Guid recordId);
}