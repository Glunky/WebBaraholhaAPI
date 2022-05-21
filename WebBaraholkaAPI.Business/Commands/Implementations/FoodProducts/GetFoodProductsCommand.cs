using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class GetFoodProductsCommand : IGetFoodProductsCommand
{
    public Task<CommandResultResponse<FoodProductResponse>> Execute(List<Guid> foodProductsIds)
    {
        throw new NotImplementedException();
    }
}