using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Second_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                schema: "public",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_username",
                schema: "public",
                table: "users");
        }
    }
}
