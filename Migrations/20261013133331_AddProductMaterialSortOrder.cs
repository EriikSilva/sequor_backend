using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sequor_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProductMaterialSortOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "ProductMaterial",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "aaa", "abc" },
                column: "SortOrder",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "bbb", "abc" },
                column: "SortOrder",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "bbb", "def" },
                column: "SortOrder",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "ccc", "def" },
                column: "SortOrder",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "ProductMaterial");
        }
    }
}
