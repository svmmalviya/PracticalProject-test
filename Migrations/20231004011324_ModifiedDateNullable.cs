using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticalProject.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedDateNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4f5e9904-de60-4978-a263-cf6ce0ed84f4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f7b6b78d-3b4e-4e57-bfef-b45bf2db8929"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { new Guid("0ac791ea-c4b0-4128-8382-de5090430b86"), "Samsung-battery" },
                    { new Guid("6632c999-8398-4a17-98c3-e4797b4f64ba"), "Nokia-battery" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0ac791ea-c4b0-4128-8382-de5090430b86"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("6632c999-8398-4a17-98c3-e4797b4f64ba"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { new Guid("4f5e9904-de60-4978-a263-cf6ce0ed84f4"), "Samsung-battery" },
                    { new Guid("f7b6b78d-3b4e-4e57-bfef-b45bf2db8929"), "Nokia-battery" }
                });
        }
    }
}
