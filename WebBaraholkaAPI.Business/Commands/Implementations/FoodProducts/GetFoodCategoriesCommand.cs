using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class GetFoodProductsCategoriesCommand : IGetFoodProductsCategoriesCommand
{
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly IDbFoodCategoryToFoodCategoryResponseMapper _foodCategoryResponseMapper;
    
    public GetFoodProductsCategoriesCommand
    (
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] IDbFoodCategoryToFoodCategoryResponseMapper foodCategoryResponseMapper
    )
    {
        _foodProductsRepository = foodProductsRepository;
        _foodCategoryResponseMapper = foodCategoryResponseMapper;
    }
    
    public async Task<CommandResultResponse<List<FoodCategoryResponse>>> Execute(int[] foodProductsCategories)
    {
        List<DbFoodProductCategory>? result = await _foodProductsRepository.GetFoodCategories(foodProductsCategories);

        if (result == null)
        {
            return new()
            {
                Body = null,
                Status = CommandResultStatus.Failed,
                Errors = new() {"Cannot find food categories with these ids"}
            };
        }

        return new()
        {
            Body = result.Select(fc => _foodCategoryResponseMapper.Map(fc)).ToList(),
            Status = CommandResultStatus.Succeed
        };
    }
}