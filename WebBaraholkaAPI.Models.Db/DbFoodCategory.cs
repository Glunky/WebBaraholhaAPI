using WebBaraholkaAPI.Core.Enums;

namespace WebBaraholkaAPI.Models.Db;

public class DbFoodProductCategory
{
    public const string TableName = "FoodProductsCategories";
    public int Id { get; set; }
    public string? Description { get; set; }
    public HashSet<DbFoodProduct>? FoodProducts { get; set; } = new();
}