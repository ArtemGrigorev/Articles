using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForWork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "webforwork");

            migrationBuilder.CreateTable(
                name: "articles",
                schema: "webforwork",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                schema: "webforwork",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "webforwork",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article_tag",
                schema: "webforwork",
                columns: table => new
                {
                    tagId = table.Column<Guid>(type: "uuid", nullable: false),
                    articleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_tag", x => new { x.tagId, x.articleId });
                    table.ForeignKey(
                        name: "FK_article_tag_articles_articleId",
                        column: x => x.articleId,
                        principalSchema: "webforwork",
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_article_tag_tags_tagId",
                        column: x => x.tagId,
                        principalSchema: "webforwork",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapter_tag",
                schema: "webforwork",
                columns: table => new
                {
                    tagId = table.Column<Guid>(type: "uuid", nullable: false),
                    chapterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapter_tag", x => new { x.tagId, x.chapterId });
                    table.ForeignKey(
                        name: "FK_chapter_tag_chapters_chapterId",
                        column: x => x.chapterId,
                        principalSchema: "webforwork",
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chapter_tag_tags_tagId",
                        column: x => x.tagId,
                        principalSchema: "webforwork",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_tag_articleId",
                schema: "webforwork",
                table: "article_tag",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_chapter_tag_chapterId",
                schema: "webforwork",
                table: "chapter_tag",
                column: "chapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_tag",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "chapter_tag",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "articles",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "chapters",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "webforwork");
        }
    }
}
