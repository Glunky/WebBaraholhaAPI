using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBaraholkaAPI.Business.Commands.Interfaces.FoodProducts;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Core.Responses;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

namespace WebBaraholkaAPI.Business.Commands.Implementations.FoodProducts;

public class GetConsumedFoodProductsHistoryCommand : IGetConsumedFoodProductsHistoryCommand
{
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly UserManager<DbApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetConsumedFoodProductsHistoryCommand(
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] UserManager<DbApplicationUser> userManager,
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        _foodProductsRepository = foodProductsRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> Execute(
        string dateFrom, string dateTo, 
        int[] foodProductsCategories, string[] foodProductsNames)
    {
        DateTime recordsTimeFrom = Convert.ToDateTime(dateFrom);
        DateTime recordsTimeTo = Convert.ToDateTime(dateTo);

        DbApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        List<DbConsumedFoodProduct> consumedFoodProducts = await _foodProductsRepository.GetConsumedFoodProductsDuringTime(user.Id, recordsTimeFrom, recordsTimeTo, foodProductsCategories, foodProductsNames);

        float totalProteins = 0;
        float totalFats = 0;
        float totalCarbohydrates = 0;
        float totalEnergyValue = 0;
        
        ConsumedFoodProductsRecordsResponse response = new();

        foreach (var consumedFoodProduct in consumedFoodProducts)
        {
            DbFoodProduct foodProduct = consumedFoodProduct.FoodProduct;
            float per100gramsCoeff = consumedFoodProduct.ConsumedMass / 100;

            totalProteins += foodProduct.Proteins * per100gramsCoeff;
            totalFats += foodProduct.Fats * per100gramsCoeff;
            totalCarbohydrates += foodProduct.Carbohydrates * per100gramsCoeff;
            totalEnergyValue += foodProduct.EnergyValue * per100gramsCoeff;
            
            response.ConsumedFoodProductsInfo.Add(new ()
            {
                ConsumedFoodProductName = consumedFoodProduct.FoodProductId,
                ConsumedMass = consumedFoodProduct.ConsumedMass,
                ConsumedProteins = foodProduct.Proteins,
                ConsumedFats = foodProduct.Fats,
                ConsumedCarbohydrates = foodProduct.Carbohydrates,
                ConsumedEnergyValue = foodProduct.EnergyValue
            });
        }
        
        return new()
        {
            Body = consumedFoodProducts.Any() ? new()
            {
                FromToTheDate = $"History from {dateFrom} to {dateTo}",
                ConsumedFoodProductsRecords = response,
                TotalProteins = totalProteins,
                TotalFats = totalFats,
                TotalCarbohydrates = totalCarbohydrates,
                TotalEnergyValue = totalEnergyValue
            } : null,
            Status = CommandResultStatus.Succeed
        };
        
    }
}