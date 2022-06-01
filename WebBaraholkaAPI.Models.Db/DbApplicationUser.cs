using Microsoft.AspNetCore.Identity;

namespace WebBaraholkaAPI.Models.Db;

public class DbApplicationUser : IdentityUser
{
    public HashSet<DbConsumedFoodProductRecord> ConsumedFoodProductRecords { get; set; }
}