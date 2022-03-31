using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PDesafioLuizaLabs.Data.Migrations
{
    public partial class DatabaseUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConfirmacaoSenha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 3, 29, 23, 1, 57, 315, DateTimeKind.Local).AddTicks(4080)),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ConfirmacaoSenha", "DateCreated", "DateUpdated", "Email", "Name", "Senha" },
                values: new object[] { new Guid("ad3f3e51-5a97-4325-8f70-b7b642c0e0ec"), "7C4A8D09CA3762AF61E59520943DC26494F8941B", new DateTime(2022, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user_default@desafioluizalabs.com", "User Default", "7C4A8D09CA3762AF61E59520943DC26494F8941B" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
