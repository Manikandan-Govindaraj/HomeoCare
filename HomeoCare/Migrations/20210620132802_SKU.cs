using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class SKU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "ImportantNote",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "InvQuantity",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "NutrientContent",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "StorageInstructions",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "Suitablefor",
                table: "TBL_Products");

            migrationBuilder.RenameColumn(
                name: "StorageLife",
                table: "TBL_Products",
                newName: "SKU");

            migrationBuilder.AddColumn<decimal>(
                name: "OriginalPrice",
                table: "TBL_Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "TBL_Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityOnHand",
                table: "TBL_Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "WSPrice",
                table: "TBL_Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalPrice",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "QuantityOnHand",
                table: "TBL_Products");

            migrationBuilder.DropColumn(
                name: "WSPrice",
                table: "TBL_Products");

            migrationBuilder.RenameColumn(
                name: "SKU",
                table: "TBL_Products",
                newName: "StorageLife");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TBL_Products",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImportantNote",
                table: "TBL_Products",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvQuantity",
                table: "TBL_Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "TBL_Products",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NutrientContent",
                table: "TBL_Products",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageInstructions",
                table: "TBL_Products",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Suitablefor",
                table: "TBL_Products",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
