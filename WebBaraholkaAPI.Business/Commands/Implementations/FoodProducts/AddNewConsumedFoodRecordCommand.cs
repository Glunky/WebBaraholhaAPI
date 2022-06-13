using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class AddNewConsumedFoodRecordCommand : IAddNewConsumedFoodRecordCommand
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly UserManager<DbApplicationUser> _userManager;

    public AddNewConsumedFoodRecordCommand(
        [FromServices] IHttpContextAccessor httpContextAccessor,
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] UserManager<DbApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _foodProductsRepository = foodProductsRepository;
        _userManager = userManager;
    }
    
    public async Task<CommandResultResponse<Guid>> Execute(AddNewConsumedFoodRecordRequest request)
    {
        DbApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        Guid recordId = await _foodProductsRepository.AddConsumedFoodRecordByUser(request.ConsumedFoodProducts, user.Id);

        return new()
        {
            Body = recordId,
            Status = CommandResultStatus.Succeed
        };
    }
}