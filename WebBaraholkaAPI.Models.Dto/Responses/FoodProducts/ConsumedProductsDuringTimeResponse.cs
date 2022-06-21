namespace WebBaraholkaAPI.Models.Dto.Responses.FoodProducts;

public class ConsumedProductsDuringTimeResponse
{
    public string FromToTheDate { get; set; }
    public List<ConsumedFoodProductsRecordsResponse> ConsumedFoodProductsRecords { get; set; }
    public float TotalProteins { get; set; }
    public float TotalFats { get; set; }
    public float TotalCarbohydrates { get; set; }
    public float TotalEnergyValue { get; set; }
}