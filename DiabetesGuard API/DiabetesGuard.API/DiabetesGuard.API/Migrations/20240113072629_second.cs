using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiabetesGuard.API.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52daa3ff-f4e3-4b57-8e85-f4d3069b00f3", "AQAAAAIAAYagAAAAEJZgmpU7Ri4x619nSq/sg7dGE7xJxxEeGEKV4wGsba7eTW6rhodlwVqYVcgdQniNAg==", "c523e075-1d5c-489e-9325-a128344209b7" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "86ef1250-178e-4379-8b7b-ead954249500",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f823d1b0-7a6c-463f-a116-6a3d94f4f9b0", "AQAAAAIAAYagAAAAEMStDRgBIivT2R2oS+Gfjn/vCOtpOVbWjqPvXL4CDtSdcecoBHGIRkS/7ToGvoqhwg==", "a2040c95-9f1d-444a-b286-586373f35e17" });
        }
    }
}
