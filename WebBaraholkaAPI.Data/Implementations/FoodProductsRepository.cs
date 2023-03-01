using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBaraholkaAPI.Data.Interfaces;
using WebBaraholkaAPI.DbProvider;
using WebBaraholkaAPI.Mappers.FoodProducts.Interfaces;
using WebBaraholkaAPI.Models.Db;
using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Data.Implementations;

public class FoodProductsRepository : IFoodProductsRepository
{
    private readonly IDataProvider _provider;
    private readonly IConsumedFoodProductToDbModelMapper _consumedFoodProductToDbModelMapper;

    public FoodProductsRepository(
        [FromServices] IDataProvider provider,
        [FromServices] IConsumedFoodProductToDbModelMapper consumedFoodProductToDbModelMapper)
    {
        _provider = provider;
        _consumedFoodProductToDbModelMapper = consumedFoodProductToDbModelMapper;
    }

    public async Task<List<DbFoodProduct>?> GetFoodProducts(string[] foodProductsNames)
    {
        List<DbFoodProduct> result = new();

        foreach (var foodProductName in foodProductsNames)
        {
            DbFoodProduct? dbFoodProduct =  await _provider.FoodProducts.FindAsync(foodProductName);

            if (dbFoodProduct == null)
            {
                return null;
            }
            
            result.Add(dbFoodProduct);
        }
        
        return result;
    }
    
    public async Task<List<DbFoodProductCategory>?> GetFoodCategories(int[] foodProductsCategories)
    {
        if (!foodProductsCategories.Any())
        {
            return null;
        }
        
        List<DbFoodProductCategory> result = new();
        foreach (var foodProductCategory in foodProductsCategories)
        {
            DbFoodProductCategory? dbFoodProductCategory = await _provider.FoodCategories
                .Include(fc => fc.FoodProducts)
                .FirstOrDefaultAsync(fc => fc.Id == foodProductCategory);

            if (dbFoodProductCategory == null)
            {
                return null;
            }

            result.Add(dbFoodProductCategory);
        }

        return result;
    }

    public async Task<Guid> AddConsumedFoodProductsRecordByUser(List<ConsumedFoodProduct> consumedFoodProducts, string userId)
    {
        Guid recordId = Guid.NewGuid();
        await _provider.ConsumedFoodProductRecords.AddAsync(new()
        {
            Id = recordId,
            UserId = userId,
            RecordingTime = DateTime.UtcNow
        });

        await _provider.ConsumedFoodProducts.AddRangeAsync(
            consumedFoodProducts.Select(cfp => _consumedFoodProductToDbModelMapper.Map(cfp, recordId)));
        await _provider.SaveAsync();
        
        return recordId;
    }

    public async Task<List<DbConsumedFoodProduct>> GetConsumedFoodProductsDuringTime(
        string userId, DateTime dateFrom, DateTime dateTo, int[] foodProductsCategories, string[] foodProductNames)
    {
        bool hasCategories = foodProductsCategories.Any();
        bool hasNames = foodProductNames.Any();
        
        
        return await _provider.ConsumedFoodProducts
                .Include(cfp => cfp.ConsumedFoodProductsRecord)
                .Include(cfp => cfp.FoodProduct)
                .Where(cfp => 
                    cfp.ConsumedFoodProductsRecord.UserId == userId && 
                    dateFrom <= cfp.ConsumedFoodProductsRecord.RecordingTime && cfp.ConsumedFoodProductsRecord.RecordingTime <= dateTo && 
                    (hasCategories && hasNames ? foodProductsCategories.Contains(cfp.FoodProduct.FoodProductCategoryId) || foodProductNames.Contains(cfp.FoodProduct.Id)
                                                : hasCategories ? foodProductsCategories.Contains(cfp.FoodProduct.FoodProductCategoryId)
                                                                : hasNames ? foodProductNames.Contains(cfp.FoodProduct.Id) 
                                                : true))
                .ToListAsync();
    }
}