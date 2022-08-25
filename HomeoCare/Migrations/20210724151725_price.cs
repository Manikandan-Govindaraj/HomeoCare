using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WSPrice",
                table: "TBL_Products",
                newName: "SellingPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "TBL_Products",
                newName: "RetailPrice");

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 44m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 32m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 56m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 74m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 68m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 6,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 95m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 7,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 49m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 8,
                columns: new[] { "RetailPrice", "SellingPrice" },
                values: new object[] { 0m, 52m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellingPrice",
                table: "TBL_Products",
                newName: "WSPrice");

            migrationBuilder.RenameColumn(
                name: "RetailPrice",
                table: "TBL_Products",
                newName: "Price");

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 44m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 32m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 56m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 74m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 68m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 6,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 95m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 7,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 49m, 0m });

            migrationBuilder.UpdateData(
                table: "TBL_Products",
                keyColumn: "ProductID",
                keyValue: 8,
                columns: new[] { "Price", "WSPrice" },
                values: new object[] { 52m, 0m });
        }
    }
}
