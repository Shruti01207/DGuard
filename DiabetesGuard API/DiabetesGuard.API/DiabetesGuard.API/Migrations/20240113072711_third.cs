using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesGuard.API.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "320aae66-4c8c-4024-b7e4-f76d4a40e45e", "AQAAAAIAAYagAAAAEOGOZXOJd+C7yqUlgpzmTKC9tNiw2C9QDNu9NRTJ+QAlNDxyY7O85/NllPxdeDGMTQ==", "222001e2-e699-49ef-b065-c5c5daf0a4d1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52daa3ff-f4e3-4b57-8e85-f4d3069b00f3", "AQAAAAIAAYagAAAAEJZgmpU7Ri4x619nSq/sg7dGE7xJxxEeGEKV4wGsba7eTW6rhodlwVqYVcgdQniNAg==", "c523e075-1d5c-489e-9325-a128344209b7" });
        }
    }
}
