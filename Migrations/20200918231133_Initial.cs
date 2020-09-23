using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace offreService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    idAdmin = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 10, nullable: false),
                    mail = table.Column<string>(type: "varchar(45)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.idAdmin);
                });

            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    idArticleType = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nomType = table.Column<string>(type: "varchar(255)", maxLength: 30, nullable: false),
                    dateDebut = table.Column<DateTime>(nullable: false),
                    dateFin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.idArticleType);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    idClient = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false),
                    numero = table.Column<decimal>(type: "decimal(64)", nullable: false),
                    mail = table.Column<string>(type: "varchar(45)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.idClient);
                });

            migrationBuilder.CreateTable(
                name: "Discussions",
                columns: table => new
                {
                    idDiscussion = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    message = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussions", x => x.idDiscussion);
                });

            migrationBuilder.CreateTable(
                name: "AdminClient",
                columns: table => new
                {
                    idClient = table.Column<int>(nullable: false),
                    idAdmin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminClient", x => new { x.idAdmin, x.idClient });
                    table.ForeignKey(
                        name: "FK_AdminClient_Admins_idAdmin",
                        column: x => x.idAdmin,
                        principalTable: "Admins",
                        principalColumn: "idAdmin",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminClient_Clients_idClient",
                        column: x => x.idClient,
                        principalTable: "Clients",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    IdArticle = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(type: "varchar(255)", maxLength: 30, nullable: false),
                    prix = table.Column<decimal>(type: "decimal(64)", maxLength: 30, nullable: false),
                    ClientID = table.Column<int>(nullable: false),
                    ArticleTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.IdArticle);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTypes_ArticleTypeID",
                        column: x => x.ArticleTypeID,
                        principalTable: "ArticleTypes",
                        principalColumn: "idArticleType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaires",
                columns: table => new
                {
                    idCommentaire = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    message = table.Column<string>(type: "varchar(255)", nullable: true),
                    ClientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaires", x => x.idCommentaire);
                    table.ForeignKey(
                        name: "FK_Commentaires_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comptes",
                columns: table => new
                {
                    idCompte = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 50, nullable: false),
                    dateCreation = table.Column<string>(type: "varchar(255)", nullable: false),
                    dateConnexion = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comptes", x => x.idCompte);
                    table.ForeignKey(
                        name: "FK_Comptes_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionClient",
                columns: table => new
                {
                    idClient = table.Column<int>(nullable: false),
                    idDiscussion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionClient", x => new { x.idDiscussion, x.idClient });
                    table.ForeignKey(
                        name: "FK_DiscussionClient_Clients_idClient",
                        column: x => x.idClient,
                        principalTable: "Clients",
                        principalColumn: "idClient",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionClient_Discussions_idDiscussion",
                        column: x => x.idDiscussion,
                        principalTable: "Discussions",
                        principalColumn: "idDiscussion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminClient_idClient",
                table: "AdminClient",
                column: "idClient");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ArticleTypeID",
                table: "Articles",
                column: "ArticleTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ClientID",
                table: "Articles",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaires_ClientID",
                table: "Commentaires",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Comptes_ClientID",
                table: "Comptes",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionClient_idClient",
                table: "DiscussionClient",
                column: "idClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminClient");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Commentaires");

            migrationBuilder.DropTable(
                name: "Comptes");

            migrationBuilder.DropTable(
                name: "DiscussionClient");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "ArticleTypes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Discussions");
        }
    }
}
