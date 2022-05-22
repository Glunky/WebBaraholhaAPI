using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

public class AddFoodProductRequest
{
    public List<FoodProduct> FoodProducts { get; set; }
}