using Microsoft.EntityFrameworkCore.Migrations;

namespace TestBook.Migrations
{
    public partial class ToAddPublicationtoBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Publication",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publication",
                table: "Books");
        }
    }
}
