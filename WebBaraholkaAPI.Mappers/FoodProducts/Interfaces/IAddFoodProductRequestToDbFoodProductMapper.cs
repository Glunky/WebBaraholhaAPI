using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;

public interface IAddFoodProductRequestToDbFoodProductMapper
{
    public DbFoodProduct Map(FoodProduct request);
}