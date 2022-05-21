using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data;
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
    
    public async Task<CommandResultResponse<List<FoodProductResponse>>> Execute(List<Guid> foodProductsIds)
    {

        if (!foodProductsIds.Any())
        {
            return new()
            {
                Body = null,
                Status = CommandResultStatus.Failed,
                Errors = new List<string>() {"Ids is broken in empty"}
            };
        }
        
        List<DbFoodProduct> result = await _foodProductsRepository.GetFoodProducts(foodProductsIds);

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