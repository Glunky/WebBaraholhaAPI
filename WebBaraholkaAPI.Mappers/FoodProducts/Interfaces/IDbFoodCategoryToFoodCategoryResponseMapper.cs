using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;

public interface IDbFoodCategoryToFoodCategoryResponseMapper
{
    public FoodCategoryResponse Map(DbFoodCategory dbFoodCategory);
}