using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vaccine_chain_bk.Migrations
{
    /// <inheritdoc />
    public partial class updateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"),
                column: "Password",
                value: "AQAAAAIAAYagAAAAEDL+YSkFMqBlivJiv/dYGXYbh/3E446vASzts7auGMcIeNL+ZqF8kAy70DAeGbYbQg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"),
                column: "Password",
                value: "AQAAAAIAAYagAAAAECLbiaF8ayWY4vet6Vsoa/u5cJ9RV3l2xJVQ6wDMAF9eQYImsIB2SD0DLwgMRlxe2g==");
        }
    }
}
