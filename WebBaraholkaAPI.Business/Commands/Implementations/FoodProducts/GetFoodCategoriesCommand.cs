using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class GetFoodCategoriesCommand : IGetFoodCategoriesCommand
{
    private readonly IFoodCategoryRepository _foodCategoryRepository;
    private readonly IDbFoodCategoryToFoodCategoryResponseMapper _foodCategoryResponseMapper;
    
    public GetFoodCategoriesCommand
    (
        [FromServices] IFoodCategoryRepository foodCategoryRepository,
        [FromServices] IDbFoodCategoryToFoodCategoryResponseMapper foodCategoryResponseMapper
    )
    {
        _foodCategoryRepository = foodCategoryRepository;
        _foodCategoryResponseMapper = foodCategoryResponseMapper;
    }
    
    public async Task<CommandResultResponse<List<FoodCategoryResponse>>> Execute(List<int> foodCategoriesIds)
    {
        List<DbFoodCategory>? result = await _foodCategoryRepository.GetFoodCategories(foodCategoriesIds);

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