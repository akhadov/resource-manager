using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Level : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "next_approver_level",
                schema: "public",
                table: "documents",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "next_approver_level",
                schema: "public",
                table: "documents");
        }
    }
}
