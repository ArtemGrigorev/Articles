using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForWork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Chapter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "chapters_tags",
                schema: "webforwork",
                columns: table => new
                {
                    chapterId = table.Column<Guid>(type: "uuid", nullable: false),
                    tagId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "IX_chapters_tags_chapterId",
                schema: "webforwork",
                table: "chapters_tags",
                column: "chapterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chapters_tags",
                schema: "webforwork");

            migrationBuilder.DropTable(
                name: "chapters",
                schema: "webforwork");
        }
    }
}
