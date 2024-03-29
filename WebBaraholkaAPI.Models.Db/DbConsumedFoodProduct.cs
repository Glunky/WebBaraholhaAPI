namespace WebBaraholkaAPI.Models.Db;

public class DbConsumedFoodProduct
{
    public const string TableName = "ConsumedFoodProducts";
    
    public Guid Id { get; set; }
    public float ConsumedMass { get; set; }
    public string FoodProductId { get; set; }
    public DbFoodProduct FoodProduct { get; set; }
    public Guid ConsumedFoodProductsRecordId { get; set; }
    public DbConsumedFoodProductsRecord ConsumedFoodProductsRecord { get; set; }
}