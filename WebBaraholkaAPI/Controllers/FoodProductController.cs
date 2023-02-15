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
    private readonly IGetFoodProductsCategoriesCommand _getFoodProductsCategoriesCommand;
    private readonly IGetFoodProductsCommand _getFoodProductsCommand;
    private readonly IAddNewConsumedFoodProductsRecordCommand _addNewConsumedFoodProductsRecordCommand;
    private readonly IGetConsumedFoodProductsHistoryCommand _getConsumedFoodProductsCommand;
    
    public FoodProductController(
        [FromServices] IGetFoodProductsCategoriesCommand getFoodProductsCategoriesCommand,
        [FromServices] IGetFoodProductsCommand getFoodProductsCommand,
        [FromServices] IAddNewConsumedFoodProductsRecordCommand addNewConsumedFoodProductsRecordCommand,
        [FromServices] IGetConsumedFoodProductsHistoryCommand getConsumedFoodProductsHistoryCommand)
    {
        _getFoodProductsCategoriesCommand = getFoodProductsCategoriesCommand;
        _getFoodProductsCommand = getFoodProductsCommand;
        _addNewConsumedFoodProductsRecordCommand = addNewConsumedFoodProductsRecordCommand;
        _getConsumedFoodProductsCommand = getConsumedFoodProductsHistoryCommand;
    }

    [HttpGet("getFoodProductsByCategories")]
    public async Task<CommandResultResponse<List<FoodCategoryResponse>>> GetFoodCategories([FromQuery(Name = "foodProductsCategories[]")] int[] foodProductsCategories)
    {
        return await _getFoodProductsCategoriesCommand.Execute(foodProductsCategories);
    }
    
    [HttpGet("getFoodProductsByNames")]
    public async Task<CommandResultResponse<List<FoodProductResponse>>> GetFoodProducts([FromQuery(Name = "foodProductsNames[]")] string[] foodProductsNames)
    {
        return await _getFoodProductsCommand.Execute(foodProductsNames);
    }

    [HttpPost("addNewConsumedFoodRecord")]
    [AddNewConsumedFoodFilter]
    public async Task<CommandResultResponse<Guid>> AddNewConsumedFoodProductsRecord([FromBody] AddNewConsumedFoodProductsRecordRequest request)
    {
        return await _addNewConsumedFoodProductsRecordCommand.Execute(request);
    }
    
    [HttpGet("getHistoryByNamesAndCategories")]
    public async Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> GetConsumedFoodProductsHistoryDuringTime(
        [FromQuery(Name = "dateFrom")] string dateFrom, 
        [FromQuery(Name = "dateTo")] string dateTo, 
        [FromQuery(Name = "foodProductsCategories[]")] int[] foodProductsCategories,
        [FromQuery(Name = "foodProductsNames[]")] string[] foodProductsNames)
    {
        return await _getConsumedFoodProductsCommand.Execute(dateFrom, dateTo, foodProductsCategories, foodProductsNames);
    }
}