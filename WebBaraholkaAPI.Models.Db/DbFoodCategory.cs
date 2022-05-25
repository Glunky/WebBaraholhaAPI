using WebBaraholkaAPI.Core.Enums;

namespace WebBaraholkaAPI.Models.Db;

public class DbFoodCategory
{
    public const string TableName = "FoodCategories";
    public int Id { get; set; }
    public string? Description { get; set; }
    public HashSet<DbFoodProduct>? FoodProducts { get; set; } = new();
}