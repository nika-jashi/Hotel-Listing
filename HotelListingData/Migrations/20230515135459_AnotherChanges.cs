using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AnotherChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "18099d58-25d3-45cb-8f6a-530c13fa8776", null, "User", "USER" },
                    { "a6642519-2b3e-4cc6-a4f1-868e125d9313", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "18099d58-25d3-45cb-8f6a-530c13fa8776");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6642519-2b3e-4cc6-a4f1-868e125d9313");

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
    }
}
