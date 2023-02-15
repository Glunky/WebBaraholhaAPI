namespace WebBaraholkaAPI.Models.Db;

public class DbFoodProduct
{
    public const string TableName = "FoodProducts";
    
    public string Id { get; set; }
    public string Description { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
    public float EnergyValue { get; set; }
    public int FoodProductCategoryId { get; set; }
    public DbFoodProductCategory FoodProductCategory { get; set; }
    public HashSet<DbConsumedFoodProduct>? ConsumedFoodProducts { get; set; }
}