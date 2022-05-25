using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetFoodCategoriesCommand
{
    public Task<CommandResultResponse<List<FoodCategoryResponse>>> Execute(List<int> foodCategoriesIds);
}