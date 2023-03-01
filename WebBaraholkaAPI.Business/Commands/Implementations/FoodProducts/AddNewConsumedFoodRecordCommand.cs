using System.Net;
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

public class AddNewConsumedFoodProductsRecordCommand : IAddNewConsumedFoodProductsRecordCommand
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly UserManager<DbApplicationUser> _userManager;

    public AddNewConsumedFoodProductsRecordCommand(
        [FromServices] IHttpContextAccessor httpContextAccessor,
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] UserManager<DbApplicationUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _foodProductsRepository = foodProductsRepository;
        _userManager = userManager;
    }
    
    public async Task<CommandResultResponse<Guid>> Execute(AddNewConsumedFoodProductsRecordRequest request)
    {
        DbApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        Guid recordId = await _foodProductsRepository.AddConsumedFoodProductsRecordByUser(request.ConsumedFoodProducts, user.Id);

        _httpContextAccessor.HttpContext.Response.StatusCode = (int) HttpStatusCode.Created;
        
        return new()
        {
            Body = recordId,
            Status = CommandResultStatus.Succeed
        };
    }
}