using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech_Invest_API.Infrastructure.Data.Migrations
{
    public partial class CriacaoTabelaTiposInvestimento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposInvestimento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuarioCriacao = table.Column<int>(type: "int", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdUsuarioAlteracao = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposInvestimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TiposInvestimento_Usuarios_IdUsuarioAlteracao",
                        column: x => x.IdUsuarioAlteracao,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TiposInvestimento_Usuarios_IdUsuarioCriacao",
                        column: x => x.IdUsuarioCriacao,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiposInvestimento_IdUsuarioAlteracao",
                table: "TiposInvestimento",
                column: "IdUsuarioAlteracao");

            migrationBuilder.CreateIndex(
                name: "IX_TiposInvestimento_IdUsuarioCriacao",
                table: "TiposInvestimento",
                column: "IdUsuarioCriacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposInvestimento");
        }
    }
}
