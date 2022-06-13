using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

public class AddNewConsumedFoodRecordRequest
{
    public List<ConsumedFoodProduct> ConsumedFoodProducts { get; set; }
}