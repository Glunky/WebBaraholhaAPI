using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using WebBaraholkaAPI.Core.Enums;
using WebBaraholkaAPI.Models.Db;

namespace WebBaraholkaAPI.DbProvider.Migrations;

[DbContext(typeof(DataContext))]
[Migration("20220518213800_AddFoodCategoriesTable")]
public class AddFoodCategoriesTable : Migration {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: DbFoodProductCategory.TableName,
            columns: table => new
            {
                Id = table.Column<int>(),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FoodProductsCategories", fp => fp.Id);
            });
    }
    
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(DbFoodProductCategory.TableName);
    }
}
