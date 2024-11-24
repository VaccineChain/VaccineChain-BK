using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace vaccine_chain_bk.Migrations
{
    /// <inheritdoc />
    public partial class addAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"), "Admin" },
                    { new Guid("be12fb13-04d6-4acf-8ad4-07927308bb6c"), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "DateOfBirth", "Email", "FirstName", "LastName", "Password", "ProfilePicture", "RoleId", "Status" },
                values: new object[] { new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"), "Admin Address", new DateTime(2002, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", "Admin", "Admin", "AQAAAAIAAYagAAAAECLbiaF8ayWY4vet6Vsoa/u5cJ9RV3l2xJVQ6wDMAF9eQYImsIB2SD0DLwgMRlxe2g==", null, new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"), 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("be12fb13-04d6-4acf-8ad4-07927308bb6c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"));
        }
    }
}
