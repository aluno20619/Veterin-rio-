


using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vet.Data.Migrations
{
    public partial class ModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 40, nullable: false),
                    Sexo = table.Column<string>(maxLength: 1, nullable: false),
                    NIF = table.Column<string>(maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donos", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Veterinarios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    NumCedulaProf = table.Column<string>(maxLength: 9, nullable: false),
                    Fotografia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarios", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Animais",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Especie = table.Column<string>(nullable: true),
                    Raca = table.Column<string>(nullable: true),
                    Peso = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: true),
                    DonoFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animais", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Animais_Donos_DonoFK",
                        column: x => x.DonoFK,
                        principalTable: "Donos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consultas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(nullable: false),
                    Observacao = table.Column<string>(nullable: true),
                    VetFK = table.Column<int>(nullable: false),
                    AnimaisFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Consultas_Animais_AnimaisFK",
                        column: x => x.AnimaisFK,
                        principalTable: "Animais",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultas_Veterinarios_VetFK",
                        column: x => x.VetFK,
                        principalTable: "Veterinarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animais_DonoFK",
                table: "Animais",
                column: "DonoFK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_AnimaisFK",
                table: "Consultas",
                column: "AnimaisFK");

            migrationBuilder.CreateIndex(
                name: "IX_Consultas_VetFK",
                table: "Consultas",
                column: "VetFK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultas");

            migrationBuilder.DropTable(
                name: "Animais");

            migrationBuilder.DropTable(
                name: "Veterinarios");

            migrationBuilder.DropTable(
                name: "Donos");
        }
    }
}
