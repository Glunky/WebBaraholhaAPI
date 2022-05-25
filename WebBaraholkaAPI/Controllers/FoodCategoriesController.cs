using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodCategoriesController : ControllerBase
{
    private readonly IGetFoodCategoriesCommand _getFoodCategoriesCommand;
    
    public FoodCategoriesController(
        [FromServices] IGetFoodCategoriesCommand getFoodCategoriesCommand)
    {
        _getFoodCategoriesCommand = getFoodCategoriesCommand;
    }

    [HttpGet("get")]
    public async Task<CommandResultResponse<List<FoodCategoryResponse>>> GetFoodCategories([FromQuery(Name = "foodCategoriesIds[]")]List<int> foodCategoriesIds)
    {
        return await _getFoodCategoriesCommand.Execute(foodCategoriesIds);
    }
}