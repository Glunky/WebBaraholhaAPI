namespace WebBaraholkaAPI.Models.Db;

public class DbConsumedFoodProductRecord
{
    public const string TableName = "ConsumedFoodProductsRecords";
    
    public Guid Id { get; set; }
    public DateTime RecordingTime { get; set; }
    public string UserId { get; set; }
    public DbApplicationUser User { get; set; }
    public HashSet<DbConsumedFoodProduct> ConsumedFoodProducts { get; set; }
}