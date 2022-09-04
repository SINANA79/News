using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsProject.Infa.Data.Sql.Migrations
{
    public partial class AddImageToNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Newses");
        }
    }
}
