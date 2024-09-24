using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_current_workflow",
                schema: "public",
                table: "workflows",
                newName: "is_checked");

            migrationBuilder.AddColumn<bool>(
                name: "is_approved",
                schema: "public",
                table: "workflows",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_approved",
                schema: "public",
                table: "workflows");

            migrationBuilder.RenameColumn(
                name: "is_checked",
                schema: "public",
                table: "workflows",
                newName: "is_current_workflow");
        }
    }
}
