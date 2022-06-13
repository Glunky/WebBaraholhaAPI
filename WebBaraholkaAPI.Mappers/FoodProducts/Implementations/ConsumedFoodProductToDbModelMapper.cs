using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Implementations;

public class ConsumedFoodProductToDbModelMapper : IConsumedFoodProductToDbModelMapper
{
    public DbConsumedFoodProduct Map(ConsumedFoodProduct consumedFoodProduct, Guid recordId)
    {
        return new ()
        {
            Id = Guid.NewGuid(), 
            ConsumedMass = consumedFoodProduct.ConsumedMass,
            FoodProductId = consumedFoodProduct.FoodName,
            ConsumedFoodProductRecordId = recordId,
        };
    }
}