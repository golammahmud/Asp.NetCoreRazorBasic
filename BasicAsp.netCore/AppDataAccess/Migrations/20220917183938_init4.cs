using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDataAccess.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produt_Category_CategoryId",
                table: "Produt");

            migrationBuilder.DropIndex(
                name: "IX_Produt_CategoryId",
                table: "Produt");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "Produt",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId1",
                table: "Produt",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produt_CategoryId1",
                table: "Produt",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Produt_Category_CategoryId1",
                table: "Produt",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produt_Category_CategoryId1",
                table: "Produt");

            migrationBuilder.DropIndex(
                name: "IX_Produt_CategoryId1",
                table: "Produt");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "Produt");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Produt",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produt_CategoryId",
                table: "Produt",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produt_Category_CategoryId",
                table: "Produt",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
