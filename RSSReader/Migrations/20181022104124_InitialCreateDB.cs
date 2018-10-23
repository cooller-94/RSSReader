using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RSSReader.Migrations
{
    public partial class InitialCreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    image_id = table.Column<Guid>(nullable: false),
                    url = table.Column<string>(nullable: false),
                    title = table.Column<string>(nullable: false),
                    link = table.Column<string>(nullable: false),
                    width = table.Column<double>(nullable: true),
                    height = table.Column<double>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.image_id);
                });

            migrationBuilder.CreateTable(
                name: "Feed",
                columns: table => new
                {
                    feed_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(nullable: false),
                    url = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    category_id = table.Column<int>(nullable: true),
                    image_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feed", x => x.feed_id);
                    table.ForeignKey(
                        name: "FK_Feed_Category_category_id",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feed_Image_image_id",
                        column: x => x.image_id,
                        principalTable: "Image",
                        principalColumn: "image_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    post_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    post_hash = table.Column<string>(nullable: true),
                    date_added = table.Column<DateTime>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    author = table.Column<string>(nullable: true),
                    comments_url = table.Column<string>(nullable: true),
                    link = table.Column<string>(nullable: true),
                    publish_date = table.Column<DateTime>(nullable: true),
                    is_read = table.Column<bool>(nullable: false),
                    feed_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.post_id);
                    table.ForeignKey(
                        name: "FK_Post_Feed_feed_id",
                        column: x => x.feed_id,
                        principalTable: "Feed",
                        principalColumn: "feed_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feed_category_id",
                table: "Feed",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Feed_image_id",
                table: "Feed",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_feed_id",
                table: "Post",
                column: "feed_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Feed");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Image");
        }
    }
}
