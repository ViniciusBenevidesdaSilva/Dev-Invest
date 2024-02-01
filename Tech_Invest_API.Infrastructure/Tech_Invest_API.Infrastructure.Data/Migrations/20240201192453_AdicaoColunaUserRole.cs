using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tech_Invest_API.Infrastructure.Data.Migrations
{
    public partial class AdicaoColunaUserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRole",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "Usuarios");
        }
    }
}
