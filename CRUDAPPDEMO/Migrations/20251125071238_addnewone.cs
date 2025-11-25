using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDAPPDEMO.Migrations
{
    /// <inheritdoc />
    public partial class addnewone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Teachers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Teachers");
        }
    }
}
