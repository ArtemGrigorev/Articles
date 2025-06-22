using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForWork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
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
                    name = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false)
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
                    name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "articles_tags",
                schema: "webforwork",
                columns: table => new
                {
                    articleId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles_tags", x => new { x.tagId, x.articleId });
                    table.ForeignKey(
                        name: "FK_articles_tags_articles_articleId",
                        column: x => x.articleId,
                        principalSchema: "webforwork",
                        principalTable: "articles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_articles_tags_tags_tagId",
                        column: x => x.tagId,
                        principalSchema: "webforwork",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapters_tags",
                schema: "webforwork",
                columns: table => new
                {
                    chapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters_tags", x => new { x.tagId, x.chapterId });
                    table.ForeignKey(
                        name: "FK_chapters_tags_chapters_chapterId",
                        column: x => x.chapterId,
                        principalSchema: "webforwork",
                        principalTable: "chapters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chapters_tags_tags_tagId",
                        column: x => x.tagId,
                        principalSchema: "webforwork",
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_articles_tags_articleId",
                schema: "webforwork",
                table: "articles_tags",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_chapters_tags_chapterId",
                schema: "webforwork",
                table: "chapters_tags",
                column: "chapterId");

            migrationBuilder.CreateIndex(
                name: "IX_tags_name",
                schema: "webforwork",
                table: "tags",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles_tags",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "chapters_tags",
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
