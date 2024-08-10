using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vaccine_chain_bk.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDbNameLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureLogs_Devices_DeviceId",
                table: "TemperatureLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureLogs_Vaccines_VaccineId",
                table: "TemperatureLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureLogs",
                table: "TemperatureLogs");

            migrationBuilder.RenameTable(
                name: "TemperatureLogs",
                newName: "Logs");

            migrationBuilder.RenameIndex(
                name: "IX_TemperatureLogs_VaccineId",
                table: "Logs",
                newName: "IX_Logs_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_TemperatureLogs_DeviceId",
                table: "Logs",
                newName: "IX_Logs_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Logs",
                table: "Logs",
                column: "TemperatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Devices_DeviceId",
                table: "Logs",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Vaccines_VaccineId",
                table: "Logs",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Devices_DeviceId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Vaccines_VaccineId",
                table: "Logs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Logs",
                table: "Logs");

            migrationBuilder.RenameTable(
                name: "Logs",
                newName: "TemperatureLogs");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_VaccineId",
                table: "TemperatureLogs",
                newName: "IX_TemperatureLogs_VaccineId");

            migrationBuilder.RenameIndex(
                name: "IX_Logs_DeviceId",
                table: "TemperatureLogs",
                newName: "IX_TemperatureLogs_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureLogs",
                table: "TemperatureLogs",
                column: "TemperatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureLogs_Devices_DeviceId",
                table: "TemperatureLogs",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureLogs_Vaccines_VaccineId",
                table: "TemperatureLogs",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "VaccineId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
