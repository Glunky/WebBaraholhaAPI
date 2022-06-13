using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider.Migrations;

[DbContext(typeof(DataContext))]
[Migration("20220519000000_AddFoodProductsTable")]
public class AddFoodProductsTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: DbFoodProduct.TableName,
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(256)", maxLength: 256),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Proteins = table.Column<float>(type: "decimal(8, 2)"),
                Fats = table.Column<float>(type: "decimal(8, 2)"),
                Carbohydrates = table.Column<float>(type: "decimal(8, 2)"),
                EnergyValue = table.Column<float>(type: "decimal(8, 2)"),
                FoodCategoryId = table.Column<int>()
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FoodProducts", fp => fp.Id);
                table.ForeignKey(
                    name: "FK_FoodProducts_FoodCategories_FoodCategoryId",
                    column: x => x.FoodCategoryId,
                    principalTable: DbFoodCategory.TableName,
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade,
                    onUpdate: ReferentialAction.Cascade);
            });
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbFoodProduct.TableName);
    }
}