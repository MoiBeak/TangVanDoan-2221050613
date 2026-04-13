using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newMVC.Migrations
{
    /// <inheritdoc />
    public partial class CreateKhoaAndSinhVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Students",
                newName: "FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_Students_FacultyID",
                table: "Students",
                newName: "IX_Students_FacultyId");

            migrationBuilder.RenameColumn(
                name: "FacultyID",
                table: "Faculties",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FacultyId",
                table: "Students",
                newName: "FacultyID");

            migrationBuilder.RenameIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                newName: "IX_Students_FacultyID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Faculties",
                newName: "FacultyID");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
