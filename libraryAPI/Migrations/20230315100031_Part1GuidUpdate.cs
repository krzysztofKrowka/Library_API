using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace libraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Part1GuidUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Author_ID",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookID",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AuthorFirstName",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorLastName",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropColumn(
                name: "Book_Id",
                table: "BookAuthors");

            migrationBuilder.AddColumn<Guid>(
                name: "Book_ID",
                table: "BookAuthors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.DropColumn(
                name: "Author_ID",
                table: "BookAuthors");
            migrationBuilder.DropUniqueConstraint("PK_BookAuthors", "BookAuthors");
            migrationBuilder.DropColumn(
                name: "ID",
                table: "BookAuthors");
            migrationBuilder.DropUniqueConstraint("PK_Authors", "Authors");

            migrationBuilder.DropColumn(
                name: "AuthorID",
                table: "Authors");

            migrationBuilder.AddColumn<Guid>(
                name: "Author_ID",
                table: "BookAuthors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "BookAuthors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorID",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "AuthorID");
            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "BookID");
            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorFirstName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorLastName",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Author_ID",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Book_ID",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Author_ID",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "BookAuthors",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "Authors",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");
        }
    }
}
