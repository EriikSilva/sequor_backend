using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sequor_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionAppointmentEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateAppointment",
                table: "Production",
                type: "DateTime",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDateAppointment",
                table: "Production");
        }
    }
}
