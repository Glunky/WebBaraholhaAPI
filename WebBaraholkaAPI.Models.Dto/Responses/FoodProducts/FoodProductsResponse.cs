namespace WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

public class FoodProductResponse
{
    public string Name { get; set; }
    public int FoodCategory { get; set; }
    public string Description { get; set; }
    public float Proteins { get; set; }
    public float Fats { get; set; }
    public float Carbohydrates { get; set; }
    public float EnergyValue { get; set; }
}