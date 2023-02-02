using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocuSignPOC2.Migrations
{
    /// <inheritdoc />
    public partial class ChangePartyEnvelopeManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Envleopes_Parties_PartyId",
                table: "Envleopes");

            migrationBuilder.DropIndex(
                name: "IX_Envleopes_PartyId",
                table: "Envleopes");

            migrationBuilder.DropColumn(
                name: "PartyId",
                table: "Envleopes");

            migrationBuilder.CreateTable(
                name: "EnvelopeParty",
                columns: table => new
                {
                    EnvelopesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PartiesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvelopeParty", x => new { x.EnvelopesId, x.PartiesId });
                    table.ForeignKey(
                        name: "FK_EnvelopeParty_Envleopes_EnvelopesId",
                        column: x => x.EnvelopesId,
                        principalTable: "Envleopes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnvelopeParty_Parties_PartiesId",
                        column: x => x.PartiesId,
                        principalTable: "Parties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnvelopeParty_PartiesId",
                table: "EnvelopeParty",
                column: "PartiesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvelopeParty");

            migrationBuilder.AddColumn<Guid>(
                name: "PartyId",
                table: "Envleopes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Envleopes_PartyId",
                table: "Envleopes",
                column: "PartyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Envleopes_Parties_PartyId",
                table: "Envleopes",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id");
        }
    }
}
