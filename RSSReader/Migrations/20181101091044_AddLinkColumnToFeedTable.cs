using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSReader.Migrations
{
    public partial class AddLinkColumnToFeedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Feeds",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Feeds");
        }
    }
}
