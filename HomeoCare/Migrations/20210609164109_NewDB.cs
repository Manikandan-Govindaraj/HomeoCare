using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomeoCare.Migrations
{
    public partial class NewDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auth");

            migrationBuilder.CreateTable(
                name: "TBL_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBD_Roles",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBD_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_UserIdentity",
                schema: "Auth",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_UserIdentity", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    ShortDesc = table.Column<string>(type: "VARCHAR(120)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(250)", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ImagePath = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Brand = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    ModelName = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    InvQuantity = table.Column<int>(nullable: false),
                    StorageLife = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    NutrientContent = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Suitablefor = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    StorageInstructions = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    ImportantNote = table.Column<string>(type: "VARCHAR(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_TBL_Products_TBL_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TBL_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_RoleBasedClaims",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_RoleBasedClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBR_RoleBasedClaims_TBD_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "TBD_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_AddressDetails",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Mobile = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    AlternateMobile = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    AddressLine1 = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    City = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    State = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    PinCode = table.Column<string>(type: "VARCHAR(10)", maxLength: 6, nullable: false),
                    Lankmark = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    addressType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_AddressDetails", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_TBL_AddressDetails_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Orders",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    DeliveredDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CourierPartner = table.Column<string>(nullable: true),
                    TrackingID = table.Column<string>(nullable: true),
                    ShippingFee = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_TBL_Orders_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_ExternalLogins",
                schema: "Auth",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_ExternalLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_TBR_ExternalLogins_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_ExternalLoginTokens",
                schema: "Auth",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_ExternalLoginTokens", x => new { x.UserID, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_TBR_ExternalLoginTokens_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_UserBasedClaims",
                schema: "Auth",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_UserBasedClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBR_UserBasedClaims_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_UserRoles",
                schema: "Auth",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_UserRoles", x => new { x.UserID, x.RoleId });
                    table.ForeignKey(
                        name: "FK_TBR_UserRoles_TBD_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Auth",
                        principalTable: "TBD_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBR_UserRoles_TBL_UserIdentity_UserID",
                        column: x => x.UserID,
                        principalSchema: "Auth",
                        principalTable: "TBL_UserIdentity",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_OrderList",
                columns: table => new
                {
                    RowID = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    OrderID = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    ProductID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_OrderList", x => x.RowID);
                    table.ForeignKey(
                        name: "FK_TBR_OrderList_TBL_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "TBL_Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBR_OrderList_TBL_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "TBL_Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBR_PaymentsDetails",
                columns: table => new
                {
                    Pay_ID = table.Column<string>(nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    rz_OrderID = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    PaymentMethod = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBR_PaymentsDetails", x => x.Pay_ID);
                    table.ForeignKey(
                        name: "FK_TBR_PaymentsDetails_TBL_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "TBL_Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_AddressDetails_UserID",
                table: "TBL_AddressDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Orders_UserID",
                table: "TBL_Orders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Products_CategoryId",
                table: "TBL_Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_OrderList_OrderID",
                table: "TBR_OrderList",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_OrderList_ProductID",
                table: "TBR_OrderList",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_PaymentsDetails_OrderID",
                table: "TBR_PaymentsDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Auth",
                table: "TBD_Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Auth",
                table: "TBL_UserIdentity",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Auth",
                table: "TBL_UserIdentity",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_ExternalLogins_UserID",
                schema: "Auth",
                table: "TBR_ExternalLogins",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_RoleBasedClaims_RoleId",
                schema: "Auth",
                table: "TBR_RoleBasedClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_UserBasedClaims_UserID",
                schema: "Auth",
                table: "TBR_UserBasedClaims",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_TBR_UserRoles_RoleId",
                schema: "Auth",
                table: "TBR_UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_AddressDetails");

            migrationBuilder.DropTable(
                name: "TBR_OrderList");

            migrationBuilder.DropTable(
                name: "TBR_PaymentsDetails");

            migrationBuilder.DropTable(
                name: "TBR_ExternalLogins",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBR_ExternalLoginTokens",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBR_RoleBasedClaims",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBR_UserBasedClaims",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBR_UserRoles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBL_Products");

            migrationBuilder.DropTable(
                name: "TBL_Orders");

            migrationBuilder.DropTable(
                name: "TBD_Roles",
                schema: "Auth");

            migrationBuilder.DropTable(
                name: "TBL_Categories");

            migrationBuilder.DropTable(
                name: "TBL_UserIdentity",
                schema: "Auth");
        }
    }
}
