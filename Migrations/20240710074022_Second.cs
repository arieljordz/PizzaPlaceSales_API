using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaPlaceSales_API.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Pizzas",
                newName: "price");

            migrationBuilder.AlterColumn<string>(
                name: "size",
                table: "Pizzas",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)",
                oldMaxLength: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Pizzas",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "size",
                table: "Pizzas",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
