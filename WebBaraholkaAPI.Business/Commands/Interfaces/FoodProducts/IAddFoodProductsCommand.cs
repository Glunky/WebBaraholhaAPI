using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IAddFoodProductsCommand
{
    Task<CommandResultResponse<List<Guid>>> Execute(AddFoodProductRequest addFoodProductRequests);
}