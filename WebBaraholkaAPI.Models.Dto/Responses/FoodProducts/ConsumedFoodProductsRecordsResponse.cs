namespace WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

public class ConsumedFoodProductsRecordsResponse
{
    public List<ConsumedFoodProductInfoResponse> ConsumedFoodProductsInfo { get; set; }

    public ConsumedFoodProductsRecordsResponse()
    {
        ConsumedFoodProductsInfo = new();
    }
}