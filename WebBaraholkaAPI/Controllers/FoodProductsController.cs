using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodProductsController : ControllerBase
{
    private readonly IGetFoodProductsCommand _getFoodProductsCommand;
    
    public FoodProductsController(
        [FromServices] IGetFoodProductsCommand getFoodProductsCommand)
    {
        _getFoodProductsCommand = getFoodProductsCommand;
    }

    [HttpGet("get")]
    public async Task<CommandResultResponse<List<FoodProductResponse>>> GetFoodProduct([FromQuery(Name = "foodProductNames[]")] List<string> foodProductNames)
    {
        return await _getFoodProductsCommand.Execute(foodProductNames);
    }
}