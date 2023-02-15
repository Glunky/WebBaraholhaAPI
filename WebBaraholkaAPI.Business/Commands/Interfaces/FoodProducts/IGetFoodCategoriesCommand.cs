using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;

public interface IGetFoodProductsCategoriesCommand
{
    public Task<CommandResultResponse<List<FoodCategoryResponse>>> Execute(int[] foodProductsCategories);
}