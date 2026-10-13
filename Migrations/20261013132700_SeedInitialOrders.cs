using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace sequor_backend.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterial",
                table: "ProductMaterial");

            migrationBuilder.UpdateData(
                table: "ProductMaterial",
                keyColumn: "MaterialCode",
                keyValue: null,
                column: "MaterialCode",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "MaterialCode",
                table: "ProductMaterial",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterial",
                table: "ProductMaterial",
                columns: new[] { "ProductCode", "MaterialCode" });

            migrationBuilder.InsertData(
                table: "Material",
                columns: new[] { "MaterialCode", "MaterialDescription" },
                values: new object[,]
                {
                    { "aaa", "desc1" },
                    { "bbb", "desc2" },
                    { "ccc", "desc3" }
                });

            migrationBuilder.InsertData(
                table: "OrderProduction",
                columns: new[] { "Order", "ProductCode", "Quantity" },
                values: new object[,]
                {
                    { "111", "abc", 100.00m },
                    { "222", "def", 200.00m }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductCode", "CycleTime", "Image", "ProductDescription" },
                values: new object[,]
                {
                    { "abc", 30.3m, "0x000001", "xxx" },
                    { "def", 45.5m, "0x00000", "yyy" }
                });

            migrationBuilder.InsertData(
                table: "ProductMaterial",
                columns: new[] { "MaterialCode", "ProductCode" },
                values: new object[,]
                {
                    { "aaa", "abc" },
                    { "bbb", "abc" },
                    { "bbb", "def" },
                    { "ccc", "def" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductMaterial",
                table: "ProductMaterial");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "MaterialCode",
                keyValue: "aaa");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "MaterialCode",
                keyValue: "bbb");

            migrationBuilder.DeleteData(
                table: "Material",
                keyColumn: "MaterialCode",
                keyValue: "ccc");

            migrationBuilder.DeleteData(
                table: "OrderProduction",
                keyColumn: "Order",
                keyValue: "111");

            migrationBuilder.DeleteData(
                table: "OrderProduction",
                keyColumn: "Order",
                keyValue: "222");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductCode",
                keyValue: "abc");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductCode",
                keyValue: "def");

            migrationBuilder.DeleteData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "aaa", "abc" });

            migrationBuilder.DeleteData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "bbb", "abc" });

            migrationBuilder.DeleteData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "bbb", "def" });

            migrationBuilder.DeleteData(
                table: "ProductMaterial",
                keyColumns: new[] { "MaterialCode", "ProductCode" },
                keyValues: new object[] { "ccc", "def" });

            migrationBuilder.AlterColumn<string>(
                name: "MaterialCode",
                table: "ProductMaterial",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductMaterial",
                table: "ProductMaterial",
                column: "ProductCode");
        }
    }
}
