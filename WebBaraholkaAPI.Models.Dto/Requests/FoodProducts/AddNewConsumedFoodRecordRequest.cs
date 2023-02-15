using WebBaraholkaAPI.Models.Dto.Models.FoodProducts;

namespace WebBaraholkaAPI.Models.Dto.Requests.FoodProducts;

public class AddNewConsumedFoodProductsRecordRequest
{
    public List<ConsumedFoodProduct> ConsumedFoodProducts { get; set; }
    public AddNewConsumedFoodProductsRecordRequest()
    {
        ConsumedFoodProducts = new();
    }
}