using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Mappers.FoodProducts.Implementations;

public class DbFoodCategoryToFoodCategoryResponseMapper : IDbFoodCategoryToFoodCategoryResponseMapper
{
    private readonly IDbFoodProductToFoodProductResponseMapper _foodProductResponseMapper;
    
    public DbFoodCategoryToFoodCategoryResponseMapper(
        [FromServices] IDbFoodProductToFoodProductResponseMapper foodProductResponseMapper
    )
    {
        _foodProductResponseMapper = foodProductResponseMapper;
    }
    
    public FoodCategoryResponse Map(DbFoodCategory dbFoodCategory)
    {
        return new()
        {
            Id = dbFoodCategory.Id,
            Description = dbFoodCategory.Description,
            FoodProducts = dbFoodCategory.FoodProducts?.Select(fp => _foodProductResponseMapper.Map(fp)).ToHashSet()
        };
    }
}