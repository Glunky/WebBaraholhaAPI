using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class GetFoodProductsCommand : IGetFoodProductsCommand
{
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly IDbFoodProductToFoodProductResponseMapper _foodProductResponseMapper;
    public GetFoodProductsCommand(
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] IDbFoodProductToFoodProductResponseMapper foodProductResponseMapper)
    {
        _foodProductsRepository = foodProductsRepository;
        _foodProductResponseMapper = foodProductResponseMapper;
    }
    
    public async Task<CommandResultResponse<List<FoodProductResponse>>> Execute(List<string> foodProductsNames)
    {
        if (!foodProductsNames.Any())
        {
            return new()
            {
                Body = null,
                Status = CommandResultStatus.Failed,
                Errors = new List<string>() {"Names is broken in empty"}
            };
        }
        
        List<DbFoodProduct>? result = await _foodProductsRepository.GetFoodProducts(foodProductsNames);

        if (result == null)
        {
             return new()
             {
                 Body = null,
                 Status = CommandResultStatus.Failed,
                 Errors = new List<string>() {"Cannot find food products with these ids"}
             };
        } 
        return new()
        {
            Body = result.Select(fp => _foodProductResponseMapper.Map(fp)).ToList(),
            Status = CommandResultStatus.Succeed
        };
    }
}