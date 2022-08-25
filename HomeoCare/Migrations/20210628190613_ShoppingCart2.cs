using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class ShoppingCart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_ShoppingCart",
                columns: table => new
                {
                    RowID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ShoppingCart", x => x.RowID);
                    table.ForeignKey(
                        name: "FK_TBL_ShoppingCart_TBL_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TBL_Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ShoppingCart_TBL_PersonalDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "TBL_PersonalDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ShoppingCart_ProductId",
                table: "TBL_ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ShoppingCart_UserID",
                table: "TBL_ShoppingCart",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ShoppingCart");
        }
    }
}
