namespace WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

public class FoodCategoryResponse
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public HashSet<FoodProductResponse>? FoodProducts { get; set; }
}