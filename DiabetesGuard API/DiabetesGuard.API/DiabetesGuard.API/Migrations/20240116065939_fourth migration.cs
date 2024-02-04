using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesGuard.API.Migrations
{
    /// <inheritdoc />
    public partial class fourthmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83874da6-d452-4155-a3b0-c999d81077d2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AQAAAAIAAYagAAAAENR7fhamf92YRR/8CgdEIUOhyjq2uFMizVKF9SM6uVMan0JJJygjnpkxgsNuwPBi0Q==", "1d54df21-40d5-41f1-90fc-065a32b1bb67" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "15168745-bd36-42ea-aa80-c736d1ee9b4a", "AQAAAAIAAYagAAAAEKCtF5qUGtYPzbZYMiDY5P99cPYU6gvWiNSNDsctEO+4P6KFTbZo1/Vw/1FNuC/LQQ==", "3316df8b-ec04-488f-8c8a-40d91d1daf28" });
        }
    }
}
