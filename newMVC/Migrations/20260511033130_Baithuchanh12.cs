using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newMVC.Migrations
{
    /// <inheritdoc />
    public partial class Baithuchanh12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
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

            migrationBuilder.CreateTable(
                name: "ExportInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExportDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceiverName = table.Column<string>(type: "TEXT", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportInvoices_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExportInvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExportInvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportInvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportInvoiceDetails_Devices_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExportInvoiceDetails_ExportInvoices_ExportInvoiceId",
                        column: x => x.ExportInvoiceId,
                        principalTable: "ExportInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImportInvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ImportInvoiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportInvoiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportInvoiceDetails_Devices_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ImportInvoiceDetails_ImportInvoices_ImportInvoiceId",
                        column: x => x.ImportInvoiceId,
                        principalTable: "ImportInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CategoryId",
                table: "Devices",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_SupplierId",
                table: "Devices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoiceDetails_ExportInvoiceId",
                table: "ExportInvoiceDetails",
                column: "ExportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoiceDetails_ProductId",
                table: "ExportInvoiceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ExportInvoices_SupplierId",
                table: "ExportInvoices",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportInvoiceDetails_ImportInvoiceId",
                table: "ImportInvoiceDetails",
                column: "ImportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportInvoiceDetails_ProductId",
                table: "ImportInvoiceDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportInvoices_SupplierId",
                table: "ImportInvoices",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportInvoiceDetails");

            migrationBuilder.DropTable(
                name: "ImportInvoiceDetails");

            migrationBuilder.DropTable(
                name: "ExportInvoices");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "ImportInvoices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
