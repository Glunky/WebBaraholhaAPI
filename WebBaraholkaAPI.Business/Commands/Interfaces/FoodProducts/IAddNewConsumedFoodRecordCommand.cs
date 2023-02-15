using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IAddNewConsumedFoodProductsRecordCommand
{
    public Task<CommandResultResponse<Guid>> Execute(AddNewConsumedFoodProductsRecordRequest request);
}