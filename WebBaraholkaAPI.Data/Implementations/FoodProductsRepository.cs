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

    public async Task<List<DbFoodProduct>?> GetFoodProducts(List<string> productNames)
    {
        List<DbFoodProduct> result = new();

        foreach (var productName in productNames)
        {
            DbFoodProduct? dbFoodProduct =  await _provider.FoodProducts.FindAsync(productName);

            if (dbFoodProduct == null)
            {
                return null;
            }
            
            result.Add(dbFoodProduct);
        }
        
        return result;
    }
    
    public async Task<List<DbFoodCategory>?> GetFoodCategories(List<int> foodCategoriesIds)
    {
        List<DbFoodCategory> result = new();
        foreach (var foodCategoryId in foodCategoriesIds)
        {
            DbFoodCategory? dbFoodCategory = await _provider.FoodCategories
                .Include(fc => fc.FoodProducts)
                .FirstOrDefaultAsync(fc => fc.Id == foodCategoryId);

            if (dbFoodCategory == null)
            {
                return null;
            }

            result.Add(dbFoodCategory);
        }

        return result;
    }

    public async Task<Guid> AddConsumedFoodRecordByUser(List<ConsumedFoodProduct> consumedFoodProducts, string UserId)
    {
        Guid recordID = Guid.NewGuid();
        await _provider.ConsumedFoodProductRecords.AddAsync(new()
        {
            Id = recordID,
            UserId = UserId,
            RecordingTime = DateTime.UtcNow
        });

        await _provider.ConsumedFoodProducts.AddRangeAsync(
            consumedFoodProducts.Select(cfp => _consumedFoodProductToDbModelMapper.Map(cfp, recordID)));

        await _provider.SaveAsync();
        
        return recordID;
    }

    public async Task<List<DbConsumedFoodProduct>> GetRecordsForUserFromTo(
        string userId, DateTime from, DateTime to, int[] foodCategories, string[] productNames)
    {
        bool hasCategories = foodCategories.Any();
        bool hasNames = productNames.Any();
        
        
        return await _provider.ConsumedFoodProducts
                .Include(cfp => cfp.ConsumedFoodProductRecord)
                .Include(cfp => cfp.FoodProduct)
                .Where(cfp => 
                    cfp.ConsumedFoodProductRecord.UserId == userId && 
                    from <= cfp.ConsumedFoodProductRecord.RecordingTime && cfp.ConsumedFoodProductRecord.RecordingTime <= to && 
                    (hasCategories && hasNames ? foodCategories.Contains(cfp.FoodProduct.FoodCategoryId) || productNames.Contains(cfp.FoodProduct.Id)
                                                : hasCategories ? foodCategories.Contains(cfp.FoodProduct.FoodCategoryId)
                                                                : hasNames ? productNames.Contains(cfp.FoodProduct.Id) 
                                                : true))
                .ToListAsync();
    }
}