using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vaccine_chain_bk.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TemperatureId",
                table: "Logs",
                newName: "LogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LogId",
                table: "Logs",
                newName: "TemperatureId");
        }
    }
}
