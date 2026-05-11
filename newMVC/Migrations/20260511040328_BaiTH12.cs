using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newMVC.Migrations
{
    /// <inheritdoc />
    public partial class BaiTH12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportInvoiceDetails_Devices_ProductId",
                table: "ExportInvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportInvoiceDetails_Devices_ProductId",
                table: "ImportInvoiceDetails");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.CreateTable(
                name: "Product1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    unit = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product1_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product1_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product1_CategoryId",
                table: "Product1",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product1_SupplierId",
                table: "Product1",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportInvoiceDetails_Product1_ProductId",
                table: "ExportInvoiceDetails",
                column: "ProductId",
                principalTable: "Product1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportInvoiceDetails_Product1_ProductId",
                table: "ImportInvoiceDetails",
                column: "ProductId",
                principalTable: "Product1",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportInvoiceDetails_Product1_ProductId",
                table: "ExportInvoiceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ImportInvoiceDetails_Product1_ProductId",
                table: "ImportInvoiceDetails");

            migrationBuilder.DropTable(
                name: "Product1");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    unit = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SupplierId",
                table: "Devices",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportInvoiceDetails_Devices_ProductId",
                table: "ExportInvoiceDetails",
                column: "ProductId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ImportInvoiceDetails_Devices_ProductId",
                table: "ImportInvoiceDetails",
                column: "ProductId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
