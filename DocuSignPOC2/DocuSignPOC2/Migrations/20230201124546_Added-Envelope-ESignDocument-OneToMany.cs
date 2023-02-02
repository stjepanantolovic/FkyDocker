using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocuSignPOC2.Migrations
{
    /// <inheritdoc />
    public partial class AddedEnvelopeESignDocumentOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ESignDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    DocuSignId = table.Column<string>(type: "text", nullable: true),
                    EnvelopeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESignDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ESignDocument_Envleopes_EnvelopeId",
                        column: x => x.EnvelopeId,
                        principalTable: "Envleopes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ESignDocument_EnvelopeId",
                table: "ESignDocument",
                column: "EnvelopeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ESignDocument");
        }
    }
}
