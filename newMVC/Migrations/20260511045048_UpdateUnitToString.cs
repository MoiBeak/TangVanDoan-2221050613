using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUnitToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unit",
                table: "Product1",
                newName: "Unit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "Product1",
                newName: "unit");
        }
    }
}
