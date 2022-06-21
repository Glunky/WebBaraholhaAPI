namespace WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

public class ConsumedFoodProductInfoResponse
{
    public string ConsumedFoodProductName { get; set; }
    public float ConsumedMass { get; set; }
    public float ConsumedProteins { get; set; }
    public float ConsumedFats { get; set; }
    public float ConsumedCarbohydrates { get; set; }
    public float ConsumedEnergyValue { get; set; }
}