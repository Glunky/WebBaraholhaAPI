using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Implementations;

public class DbFoodProductToFoodProductResponseMapper : IDbFoodProductToFoodProductResponseMapper
{
    public FoodProductResponse Map(DbFoodProduct dbFoodProduct)
    {
        return new()
        {
            Name = dbFoodProduct.Name,
            Description = dbFoodProduct.Description,
            Proteins = dbFoodProduct.Proteins,
            Fats = dbFoodProduct.Fats,
            Carbohydrates = dbFoodProduct.Carbohydrates,
            EnergyValue = dbFoodProduct.EnergyValue,
        };
    }
}