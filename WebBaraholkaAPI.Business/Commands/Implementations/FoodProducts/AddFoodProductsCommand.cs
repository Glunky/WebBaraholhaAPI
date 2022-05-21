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
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class AddFoodProductsCommand : IAddFoodProductsCommand
{
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly IAddFoodProductRequestToDbFoodProductMapper _addFoodProductMapper;

    public AddFoodProductsCommand(
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] IAddFoodProductRequestToDbFoodProductMapper addFoodProductMapper)
    {
        _foodProductsRepository = foodProductsRepository;
        _addFoodProductMapper = addFoodProductMapper;
    }

    public async Task<CommandResultResponse<List<Guid>>> Execute(AddFoodProductRequest addFoodProductRequests)
    {
        List<DbFoodProduct> dbFoodProducts = addFoodProductRequests.FoodProducts.Select(r => _addFoodProductMapper.Map(r)).ToList();
        
        await _foodProductsRepository.AddFoodProducts(dbFoodProducts);
        return new()
        {
            Status = CommandResultStatus.Succeed,
            Body = dbFoodProducts.Select(fp => fp.Id).ToList()
        };
    }
}