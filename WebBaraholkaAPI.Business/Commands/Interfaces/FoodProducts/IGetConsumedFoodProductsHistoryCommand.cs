using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetConsumedFoodProductsHistory
{
    public Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> Execute(string from, string to);
}