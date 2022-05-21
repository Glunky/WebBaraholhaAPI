using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetFoodProductsCommand
{
    Task<CommandResultResponse<List<FoodProductResponse>>> Execute(List<Guid> foodProductsIds);
}