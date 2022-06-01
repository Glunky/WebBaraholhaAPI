namespace WebBaraholkaAPI.Models.Db;

public class DbConsumedFoodProduct
{
    public const string TableName = "ConsumedFoodProducts";
    
    public Guid Id { get; set; }
    public float ConsumedMass { get; set; }
    public Guid FoodProductId { get; set; }
    public DbFoodProduct FoodProduct { get; set; }
    public Guid ConsumedFoodProductRecordId { get; set; }
    public DbConsumedFoodProductRecord ConsumedFoodProductRecord { get; set; }
}