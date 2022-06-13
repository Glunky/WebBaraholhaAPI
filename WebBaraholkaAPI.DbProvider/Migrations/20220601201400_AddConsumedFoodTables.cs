using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider.Migrations;

[DbContext(typeof(DataContext))]
[Migration("20220601201400_AddConsumedFoodTables")]
public class AddConsumedFoodTables : Migration {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        CreateConsumedFoodProductsTable(ref migrationBuilder);
        CreateConsumedFoodProductRecordsTable(ref migrationBuilder);
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbConsumedFoodProductRecord.TableName);
        migrationBuilder.DropTable(DbConsumedFoodProduct.TableName);
    }

    private void CreateConsumedFoodProductsTable(ref MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: DbConsumedFoodProduct.TableName,
            columns: table => new
            {
                Id = table.Column<Guid>(),
                ConsumedMass = table.Column<float>(type: "decimal(8, 2)"),
                FoodProductId = table.Column<string>(type: "nvarchar(256)", maxLength: 256),
                ConsumedFoodProductRecordId = table.Column<Guid>(),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ConsumedFoodProducts", cfp => cfp.Id);
            });
    }
    
    private void CreateConsumedFoodProductRecordsTable(ref MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: DbConsumedFoodProductRecord.TableName,
            columns: table => new
            {
                Id = table.Column<Guid>(),
                RecordingTime = table.Column<DateTime>(type: "date"),
                UserId = table.Column<string>("nvarchar(450)"),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ConsumedFoodProductsRecords", cfpr => cfpr.Id);
            });
    }
}