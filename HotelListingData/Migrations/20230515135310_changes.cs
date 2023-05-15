using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListingAPI.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22615cd9-a7c1-4125-a0c8-474d5d2d0bcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "704fbaff-646d-4346-9eac-d83a54f41852");

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9ec91ef0-8c24-484b-a480-93b54e0f32fe", null, "User", "USER" },
                    { "b5d7af67-d2c0-4cd9-893c-9d2d33d8c1c2", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ec91ef0-8c24-484b-a480-93b54e0f32fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5d7af67-d2c0-4cd9-893c-9d2d33d8c1c2");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22615cd9-a7c1-4125-a0c8-474d5d2d0bcd", null, "Administrator", "ADMINISTRATOR" },
                    { "704fbaff-646d-4346-9eac-d83a54f41852", null, "User", "USER" }
                });
        }
    }
}
