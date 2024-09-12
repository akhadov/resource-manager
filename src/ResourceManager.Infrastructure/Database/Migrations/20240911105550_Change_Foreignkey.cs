using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResourceManager.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Change_Foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_histories_documents_id",
                schema: "public",
                table: "histories");

            migrationBuilder.CreateIndex(
                name: "ix_histories_document_id",
                schema: "public",
                table: "histories",
                column: "document_id");

            migrationBuilder.AddForeignKey(
                name: "fk_histories_documents_document_id",
                schema: "public",
                table: "histories",
                column: "document_id",
                principalSchema: "public",
                principalTable: "documents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_histories_documents_document_id",
                schema: "public",
                table: "histories");

            migrationBuilder.DropIndex(
                name: "ix_histories_document_id",
                schema: "public",
                table: "histories");

            migrationBuilder.AddForeignKey(
                name: "fk_histories_documents_id",
                schema: "public",
                table: "histories",
                column: "id",
                principalSchema: "public",
                principalTable: "documents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
