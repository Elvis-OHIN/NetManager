using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetManager.Migrations
{
    /// <inheritdoc />
    public partial class index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Workstation_CarteMere",
                table: "Workstation",
                column: "CarteMere",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Workstation_CarteMere",
                table: "Workstation");
        }
    }
}
