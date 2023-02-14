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

public class GetConsumedFoodProductsCommand : IGetConsumedFoodProducts
{
    private readonly IFoodProductsRepository _foodProductsRepository;
    private readonly UserManager<DbApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetConsumedFoodProductsCommand(
        [FromServices] IFoodProductsRepository foodProductsRepository,
        [FromServices] UserManager<DbApplicationUser> userManager,
        [FromServices] IHttpContextAccessor httpContextAccessor)
    {
        _foodProductsRepository = foodProductsRepository;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<CommandResultResponse<ConsumedProductsDuringTimeResponse>> Execute(
        string from, string to, 
        int[] foodCategories, string[] foodNames)
    {
        DateTime recordsTimeFrom = Convert.ToDateTime(from);
        DateTime recordsTimeTo = Convert.ToDateTime(to);

        DbApplicationUser user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

        List<DbConsumedFoodProduct> recordsFromTo = await _foodProductsRepository.GetRecordsForUserFromTo(user.Id, recordsTimeFrom, recordsTimeTo, foodCategories, foodNames);

        float totalProteins = 0;
        float totalFats = 0;
        float totalCarbohydrates = 0;
        float totalEnergyValue = 0;
        
        ConsumedFoodProductsRecordsResponse response = new();
        response.ConsumedFoodProductsInfo = new();
        
        foreach (var consumedFoodProduct in recordsFromTo)
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
        
        if (recordsFromTo.Any())
        {
            return new()
            {
                Body = new()
                {
                    FromToTheDate = $"History from {from} to {to}",
                    ConsumedFoodProductsRecords = response,
                    TotalProteins = totalProteins,
                    TotalFats = totalFats,
                    TotalCarbohydrates = totalCarbohydrates,
                    TotalEnergyValue = totalEnergyValue
                },
                Status = CommandResultStatus.Succeed
            };
        }

        return new()
        {
            Body = null,
            Status = CommandResultStatus.Succeed
        };
    }
}