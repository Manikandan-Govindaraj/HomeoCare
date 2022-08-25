using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class razorpayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "TBR_OrderList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RazorPayID",
                table: "TBL_PersonalDetails",
                type: "VARCHAR(25)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "TBR_OrderList");

            migrationBuilder.DropColumn(
                name: "RazorPayID",
                table: "TBL_PersonalDetails");
        }
    }
}
