using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodProductController : ControllerBase
{
    private readonly IGetFoodCategoriesCommand _getFoodCategoriesCommand;
    private readonly IGetFoodProductsCommand _getFoodProductsCommand;
    private readonly IAddNewConsumedFoodRecordCommand _addNewConsumedFoodRecordCommand;
    
    public FoodProductController(
        [FromServices] IGetFoodCategoriesCommand getFoodCategoriesCommand,
        [FromServices] IGetFoodProductsCommand getFoodProductsCommand,
        [FromServices] IAddNewConsumedFoodRecordCommand addNewConsumedFoodRecordCommand)
    {
        _getFoodCategoriesCommand = getFoodCategoriesCommand;
        _getFoodProductsCommand = getFoodProductsCommand;
        _addNewConsumedFoodRecordCommand = addNewConsumedFoodRecordCommand;
    }

    [HttpGet("getCategories")]
    public async Task<CommandResultResponse<List<FoodCategoryResponse>>> GetFoodCategories([FromQuery(Name = "foodCategoriesIds[]")]List<int> foodCategoriesIds)
    {
        return await _getFoodCategoriesCommand.Execute(foodCategoriesIds);
    }
    
    [HttpGet("getProducts")]
    public async Task<CommandResultResponse<List<FoodProductResponse>>> GetFoodProducts([FromQuery(Name = "foodProductNames[]")] List<string> foodProductNames)
    {
        return await _getFoodProductsCommand.Execute(foodProductNames);
    }

    [HttpPost("addNewConsumedFoodRecord")]
    [AddNewConsumedFoodFilter]
    public async Task<CommandResultResponse<Guid>> AddNewConsumedFoodRecord([FromBody] AddNewConsumedFoodRecordRequest request)
    {
        return await _addNewConsumedFoodRecordCommand.Execute(request);
    }
}