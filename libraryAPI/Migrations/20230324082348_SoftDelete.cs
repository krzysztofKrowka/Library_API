using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LibrarianID",
                table: "Librarians",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "Books",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Authors",
                newName: "ID");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Librarians",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Librarians");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Librarians",
                newName: "LibrarianID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Books",
                newName: "BookID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Authors",
                newName: "AuthorID");
        }
    }
}
