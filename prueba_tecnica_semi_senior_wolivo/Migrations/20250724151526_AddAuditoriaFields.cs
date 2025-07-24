using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prueba_tecnica_semi_senior_wolivo.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditoriaFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Usuario",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Proyecto",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Proyecto",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Proyecto",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Actividad",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Actividad",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "Actividad",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Proyecto");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Actividad");

            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "Actividad");

            migrationBuilder.AlterColumn<int>(
                name: "Rol",
                table: "Usuario",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Estado",
                table: "Usuario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Proyecto",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Estado",
                table: "Actividad",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
