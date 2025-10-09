using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CC4CC3195CA0", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Provider = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Identify = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__349DA5A6BAD6BD3F", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK__Account__UserId__3B75D760",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    TokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tmp_ms_x__658FEEEABAD99E56", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK__RefreshTo__UserI__5EBF139D",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "Email", "Name" },
                values: new object[] { 1, "demo@gmail.com", "Demo" });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "Identify", "PasswordHash", "Provider", "UserId" },
                values: new object[] { 1, "demo@gmail.com", "AQAAAAIAAYagAAAAEJnHg+CIGqb8SRQcT9MpZbU11FyRYuyxzTJkDEHG9mG6rkmAaDkODwYOCVbiW+kxyw==", "Local", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                table: "Account",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__Account__349DA5A7AEAE707B",
                table: "Account",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__tmp_ms_x__658FEEEB8DC1D79C",
                table: "RefreshToken",
                column: "TokenId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__User__1788CC4D882CE5A2",
                table: "User",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
