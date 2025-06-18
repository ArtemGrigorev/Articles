using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebForWork.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adduniq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "webforwork",
                table: "tags",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "webforwork",
                table: "chapters",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "webforwork",
                table: "articles",
                newName: "name");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "webforwork",
                table: "chapters",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "webforwork",
                table: "articles",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
            migrationBuilder.DropIndex(
                name: "IX_tags_name",
                schema: "webforwork",
                table: "tags");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "webforwork",
                table: "tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "webforwork",
                table: "chapters",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "webforwork",
                table: "articles",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "webforwork",
                table: "chapters",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "webforwork",
                table: "articles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);
        }
    }
}
