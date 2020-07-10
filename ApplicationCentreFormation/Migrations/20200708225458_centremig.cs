using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApplicationCentreFormation.Migrations
{
    public partial class centremig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidat",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nom = table.Column<string>(nullable: false),
                    prenom = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    cin = table.Column<string>(nullable: false),
                    photo = table.Column<string>(nullable: false),
                    cv = table.Column<string>(nullable: false),
                    mot_pass = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formateur",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nom = table.Column<string>(nullable: false),
                    prenom = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    tel = table.Column<string>(nullable: false),
                    cin = table.Column<string>(nullable: false),
                    tarif_horaire = table.Column<string>(nullable: false),
                    photo = table.Column<string>(nullable: false),
                    cv = table.Column<string>(nullable: false),
                    mot_pass = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formateur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveau",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialite",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    nom = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    titre = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    charge_horaire = table.Column<string>(nullable: false),
                    programme = table.Column<string>(nullable: false),
                    NiveauId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NiveauFormation",
                        column: x => x.NiveauId,
                        principalTable: "Niveau",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormateurSpecialite",
                columns: table => new
                {
                    Formateur_Id = table.Column<Guid>(nullable: false),
                    Specialite_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormateurSpecialite", x => new { x.Formateur_Id, x.Specialite_Id });
                    table.ForeignKey(
                        name: "FK_FormateurSpecialite_Formateur",
                        column: x => x.Formateur_Id,
                        principalTable: "Formateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormateurSpecialite_Specialite",
                        column: x => x.Specialite_Id,
                        principalTable: "Specialite",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    date_deb = table.Column<DateTime>(type: "datetime", nullable: false),
                    date_fin = table.Column<DateTime>(type: "datetime", nullable: false),
                    planning = table.Column<string>(nullable: false),
                    FormationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormationSession",
                        column: x => x.FormationId,
                        principalTable: "Formation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionCandidat",
                columns: table => new
                {
                    Session_Id = table.Column<Guid>(nullable: false),
                    Candidat_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCandidat", x => new { x.Session_Id, x.Candidat_Id });
                    table.ForeignKey(
                        name: "FK_SessionCandidat_Candidat",
                        column: x => x.Candidat_Id,
                        principalTable: "Candidat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionCandidat_Session",
                        column: x => x.Session_Id,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionFormateur",
                columns: table => new
                {
                    Session_Id = table.Column<Guid>(nullable: false),
                    Formateur_Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionFormateur", x => new { x.Session_Id, x.Formateur_Id });
                    table.ForeignKey(
                        name: "FK_SessionFormateur_Formateur",
                        column: x => x.Formateur_Id,
                        principalTable: "Formateur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SessionFormateur_Session",
                        column: x => x.Session_Id,
                        principalTable: "Session",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FK_FormateurSpecialite_Specialite",
                table: "FormateurSpecialite",
                column: "Specialite_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FK_NiveauFormation",
                table: "Formation",
                column: "NiveauId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_FormationSession",
                table: "Session",
                column: "FormationId");

            migrationBuilder.CreateIndex(
                name: "IX_FK_SessionCandidat_Candidat",
                table: "SessionCandidat",
                column: "Candidat_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FK_SessionFormateur_Formateur",
                table: "SessionFormateur",
                column: "Formateur_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormateurSpecialite");

            migrationBuilder.DropTable(
                name: "SessionCandidat");

            migrationBuilder.DropTable(
                name: "SessionFormateur");

            migrationBuilder.DropTable(
                name: "Specialite");

            migrationBuilder.DropTable(
                name: "Candidat");

            migrationBuilder.DropTable(
                name: "Formateur");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "Formation");

            migrationBuilder.DropTable(
                name: "Niveau");
        }
    }
}
