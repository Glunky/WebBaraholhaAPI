using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Filters.FoodProducts;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoodProductsController : ControllerBase
{
    private readonly IAddFoodProductsCommand _addFoodProductsCommand;
    
    public FoodProductsController([FromServices] IAddFoodProductsCommand addFoodProductsCommand)
    {
        _addFoodProductsCommand = addFoodProductsCommand;
    }
    
    [HttpPost("create")]
    [AddFoodProductFilter]
    public async Task<CommandResultResponse<List<Guid>>> CreateNewFoodProduct(AddFoodProductRequest request)
    {
        return await _addFoodProductsCommand.Execute(request);
    }
}