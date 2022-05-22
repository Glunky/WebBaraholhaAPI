using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;

public interface IAddFoodProductRequestToDbFoodProductMapper
{
    public DbFoodProduct Map(FoodProduct request);
}