using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class seedprod1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TBL_Products",
                type: "VARCHAR(350)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "TBL_Products",
                columns: new[] { "ProductID", "CategoryId", "Description", "ImagePath", "OriginalPrice", "Price", "ProductName", "Quantity", "QuantityOnHand", "SKU", "ShortDesc", "WSPrice" },
                values: new object[] { 1, 1, "Looking after your skins health is of utmost importance because extra effort always shows, even on your skin. Thats why you should pay extra attention to what you choose for your skin. Introducing the Vaseline Ice cool Hydration lotion which rapidly cools skin by -3 degree C.", "04e4c6e4-acd4-4f06-8d83-0b0b5370a01f.jfif", 0m, 44m, "Hydration Lotion", 10, 10, null, "his range also consists of the Vaseline with Body Ice Cream 165g which is a gel crme.", 0m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TBL_Products",
                type: "VARCHAR(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(350)",
                oldMaxLength: 250,
                oldNullable: true);
        }
    }
}
