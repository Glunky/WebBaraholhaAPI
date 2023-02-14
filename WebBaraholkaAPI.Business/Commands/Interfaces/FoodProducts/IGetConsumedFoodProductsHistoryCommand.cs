using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetConsumedFoodProducts
{
    public Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> Execute(
        string from, string to, 
        int[] foodCategory, string[] foodNames);
}