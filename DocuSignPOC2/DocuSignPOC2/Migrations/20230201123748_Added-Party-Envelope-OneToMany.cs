using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocuSignPOC2.Migrations
{
    /// <inheritdoc />
    public partial class AddedPartyEnvelopeOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Envleopes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PartyId = table.Column<Guid>(type: "uuid", nullable: true),
                    DocuSignId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envleopes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envleopes_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Envleopes_PartyId",
                table: "Envleopes",
                column: "PartyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Envleopes");
        }
    }
}
