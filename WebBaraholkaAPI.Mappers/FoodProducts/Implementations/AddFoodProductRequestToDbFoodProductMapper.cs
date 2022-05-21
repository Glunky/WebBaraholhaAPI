using System;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Implementations;

public class AddFoodProductRequestToDbFoodProductMapper : IAddFoodProductRequestToDbFoodProductMapper
{
    public DbFoodProduct Map(FoodProduct request)
    {
        return new DbFoodProduct()
        {
            Id = Guid.NewGuid(), 
            Name = request.Name,
            Description = request.Description,
            Proteins = request.Proteins,
            Fats = request.Fats,
            Carbohydrates = request.Carbohydrates,
            EnergyValue = request.EnergyValue
        };
    }
}