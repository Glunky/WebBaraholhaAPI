using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;

public interface IDbFoodProductToFoodProductResponseMapper
{
    public FoodProductResponse Map(DbFoodProduct dbFoodProduct);
}