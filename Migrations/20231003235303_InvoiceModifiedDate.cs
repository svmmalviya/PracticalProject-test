using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PracticalProject.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceModifiedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("ac2aa36d-a072-4a66-a7f6-9108d20a2649"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f6035a09-0628-4ef2-93a0-3589173cab25"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { new Guid("4f5e9904-de60-4978-a263-cf6ce0ed84f4"), "Samsung-battery" },
                    { new Guid("f7b6b78d-3b4e-4e57-bfef-b45bf2db8929"), "Nokia-battery" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("4f5e9904-de60-4978-a263-cf6ce0ed84f4"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("f7b6b78d-3b4e-4e57-bfef-b45bf2db8929"));

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Invoices");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductName" },
                values: new object[,]
                {
                    { new Guid("ac2aa36d-a072-4a66-a7f6-9108d20a2649"), "Samsung-battery" },
                    { new Guid("f6035a09-0628-4ef2-93a0-3589173cab25"), "Nokia-battery" }
                });
        }
    }
}
