using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UltimateWebAPIWorkSpace.Migrations
{
    public partial class InitialRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b5ba71f-6eee-4d14-8e26-6c82648a64e6", "6d6ad2ed-055b-4625-bb34-0944ffa847a9", "Manager", "MANAGER" },
                    { "a89d08d0-3a37-4352-980d-51ac48ee8e36", "cad13fa5-b110-44f1-84e6-d576670393a2", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b5ba71f-6eee-4d14-8e26-6c82648a64e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a89d08d0-3a37-4352-980d-51ac48ee8e36");
        }
    }
}
