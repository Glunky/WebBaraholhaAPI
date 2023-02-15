using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBaraholkaAPI.DbProvider.Migrations;


[DbContext(typeof(DataContext))]
[Migration("20220601220254_AddConstrains")]
public class AddConstrains : Migration {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddForeignKey(
            name: "FK_ConsumedFoodProductsRecords_AspNetUsers_UserId",
            table: "ConsumedFoodProductsRecords",
            column: "UserId",
            principalTable: "AspNetUsers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade,
            onUpdate: ReferentialAction.Cascade
        );
        
        migrationBuilder.AddForeignKey(
            name: "FK_ConsumedFoodProducts_FoodProducts_FoodProductId",
            table: "ConsumedFoodProducts",
            column: "FoodProductId",
            principalTable: "FoodProducts",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict,
            onUpdate: ReferentialAction.Cascade
        );
        
        migrationBuilder.AddForeignKey(
            name: "FK_ConsumedFoodProducts_ConsumedFoodProductsRecords_ConsumedFoodProductsRecordId",
            table: "ConsumedFoodProducts",
            column: "ConsumedFoodProductsRecordId",
            principalTable: "ConsumedFoodProductsRecords",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade,
            onUpdate: ReferentialAction.Cascade
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "FK_ConsumedFoodProducts_ConsumedFoodProductsRecords_ConsumedFoodProductsRecordId", "ConsumedFoodProducts");
        migrationBuilder.DropForeignKey(
            "FK_ConsumedFoodProducts_FoodProducts_FoodProductId", "ConsumedFoodProducts");
        migrationBuilder.DropForeignKey(
            "FK_ConsumedFoodProductsRecords_AspNetUsers_UserId", "ConsumedFoodProductsRecords");
    }
}