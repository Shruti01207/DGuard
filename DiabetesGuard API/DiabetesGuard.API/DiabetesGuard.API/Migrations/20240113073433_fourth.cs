using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesGuard.API.Migrations
{
    /// <inheritdoc />
    public partial class fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "SecurityStamp", "TokenCreated", "TokenExpires" },
                values: new object[] { "15168745-bd36-42ea-aa80-c736d1ee9b4a", "AQAAAAIAAYagAAAAEKCtF5qUGtYPzbZYMiDY5P99cPYU6gvWiNSNDsctEO+4P6KFTbZo1/Vw/1FNuC/LQQ==", "", "3316df8b-ec04-488f-8c8a-40d91d1daf28", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "320aae66-4c8c-4024-b7e4-f76d4a40e45e", "AQAAAAIAAYagAAAAEOGOZXOJd+C7yqUlgpzmTKC9tNiw2C9QDNu9NRTJ+QAlNDxyY7O85/NllPxdeDGMTQ==", "222001e2-e699-49ef-b065-c5c5daf0a4d1" });
        }
    }
}
