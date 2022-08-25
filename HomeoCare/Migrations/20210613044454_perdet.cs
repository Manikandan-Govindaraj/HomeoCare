using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class perdet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TBL_PersonalDetails",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "TBL_PersonalDetails");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "TBL_UserIdentity",
                column: "NormalizedEmail");
        }
    }
}
