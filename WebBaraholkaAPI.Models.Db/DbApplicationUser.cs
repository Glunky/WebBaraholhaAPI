using Microsoft.AspNetCore.Identity;

namespace WebBaraholkaAPI.Models.Db;

public class DbApplicationUser : IdentityUser
{
    public HashSet<DbConsumedFoodProductsRecord> ConsumedFoodProductsRecords { get; set; }
}