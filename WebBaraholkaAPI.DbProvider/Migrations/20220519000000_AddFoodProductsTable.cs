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
                Id = table.Column<Guid>(),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Proteins = table.Column<float>(type: "decimal(8, 2)"),
                Fats = table.Column<float>(type: "decimal(8, 2)"),
                Carbohydrates = table.Column<float>(type: "decimal(8, 2)"),
                EnergyValue = table.Column<float>(type: "decimal(8, 2)")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FoodProducts", fp => fp.Id);
                table.UniqueConstraint("UC_FoodProducts_Unique", fp => fp.Name);
            });
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbFoodProduct.TableName);
    }
}