using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class personaldetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_AddressDetails_TBL_UserIdentity_UserID",
                table: "TBL_AddressDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_UserIdentity_UserID",
                table: "TBL_Orders");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "Auth",
                table: "TBL_UserIdentity");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "Auth",
                table: "TBL_UserIdentity",
                newName: "DisplayName");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Auth",
                table: "TBL_UserIdentity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateTable(
                name: "TBL_PersonalDetails",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(30)", maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PersonalDetails", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_TBL_PersonalDetails_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_AddressDetails_TBL_PersonalDetails_UserID",
                table: "TBL_AddressDetails",
                column: "UserID",
                principalTable: "TBL_PersonalDetails",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_PersonalDetails_UserID",
                table: "TBL_Orders",
                column: "UserID",
                principalTable: "TBL_PersonalDetails",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_AddressDetails_TBL_PersonalDetails_UserID",
                table: "TBL_AddressDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Orders_TBL_PersonalDetails_UserID",
                table: "TBL_Orders");

            migrationBuilder.DropTable(
                name: "TBL_PersonalDetails");

            migrationBuilder.RenameColumn(
                name: "DisplayName",
                schema: "Auth",
                table: "TBL_UserIdentity",
                newName: "LastName");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "VARCHAR(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "Auth",
                table: "TBL_UserIdentity",
                type: "VARCHAR(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_AddressDetails_TBL_UserIdentity_UserID",
                table: "TBL_AddressDetails",
                column: "UserID",
                principalSchema: "Auth",
                principalTable: "TBL_UserIdentity",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Orders_TBL_UserIdentity_UserID",
                table: "TBL_Orders",
                column: "UserID",
                principalSchema: "Auth",
                principalTable: "TBL_UserIdentity",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
