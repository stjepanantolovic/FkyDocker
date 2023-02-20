using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocuSignPOC2.Migrations
{
    /// <inheritdoc />
    public partial class AddedWebHooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ESignDocument_Envleopes_EnvelopeId",
                table: "ESignDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ESignDocument",
                table: "ESignDocument");

            migrationBuilder.RenameTable(
                name: "ESignDocument",
                newName: "ESignDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_ESignDocument_EnvelopeId",
                table: "ESignDocuments",
                newName: "IX_ESignDocuments_EnvelopeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ESignDocuments",
                table: "ESignDocuments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WebHooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Json = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHooks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ESignDocuments_Envleopes_EnvelopeId",
                table: "ESignDocuments",
                column: "EnvelopeId",
                principalTable: "Envleopes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ESignDocuments_Envleopes_EnvelopeId",
                table: "ESignDocuments");

            migrationBuilder.DropTable(
                name: "WebHooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ESignDocuments",
                table: "ESignDocuments");

            migrationBuilder.RenameTable(
                name: "ESignDocuments",
                newName: "ESignDocument");

            migrationBuilder.RenameIndex(
                name: "IX_ESignDocuments_EnvelopeId",
                table: "ESignDocument",
                newName: "IX_ESignDocument_EnvelopeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ESignDocument",
                table: "ESignDocument",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ESignDocument_Envleopes_EnvelopeId",
                table: "ESignDocument",
                column: "EnvelopeId",
                principalTable: "Envleopes",
                principalColumn: "Id");
        }
    }
}
