using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDataAccess.Migrations
{
    public partial class multipleCheckboxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCheckboxId",
                table: "MultipleCheckbox");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCheckboxId",
                table: "MultipleCheckbox",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
