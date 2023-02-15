using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetConsumedFoodProductsHistoryCommand
{
    public Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> Execute(string dateFrom, string dateTo, int[] foodProductsCategories, string[] foodProductsNames);
}