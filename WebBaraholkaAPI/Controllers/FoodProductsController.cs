using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodProductsController : ControllerBase
{
    private readonly IAddFoodProductsCommand _addFoodProductsCommand;
    private readonly IGetFoodProductsCommand _getFoodProductsCommand;
    
    public FoodProductsController(
        [FromServices] IAddFoodProductsCommand addFoodProductsCommand,
        [FromServices] IGetFoodProductsCommand getFoodProductsCommand)
    {
        _addFoodProductsCommand = addFoodProductsCommand;
        _getFoodProductsCommand = getFoodProductsCommand;
    }
    
    [HttpPost("create")]
    [AddFoodProductFilter]
    public async Task<CommandResultResponse<List<Guid>>> CreateNewFoodProduct(AddFoodProductRequest request)
    {
        return await _addFoodProductsCommand.Execute(request);
    }

    [HttpGet("get")]
    public async Task<CommandResultResponse<List<FoodProductResponse>>> GetFoodProduct([FromQuery(Name = "foodproductids[]")] List<Guid> foodProductIds)
    {
        return await _getFoodProductsCommand.Execute(foodProductIds);
    }
}