using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sequor_backend.Migrations
{
    /// <inheritdoc />
    public partial class cycleTimeElapsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CycleTimeElapsed",
                table: "Production",
                type: "numeric(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CycleTimeElapsed",
                table: "Production");
        }
    }
}
