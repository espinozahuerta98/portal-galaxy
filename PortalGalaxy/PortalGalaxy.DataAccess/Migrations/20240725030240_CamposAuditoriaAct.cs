using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalGalaxy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CamposAuditoriaAct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Taller",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualizacion",
                table: "Taller",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Instructor",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualizacion",
                table: "Instructor",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Inscripcion",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualizacion",
                table: "Inscripcion",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Categoria",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualizacion",
                table: "Categoria",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaActualizacion",
                table: "Alumno",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioActualizacion",
                table: "Alumno",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualizacion", "UsuarioActualizacion" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualizacion", "UsuarioActualizacion" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaActualizacion", "UsuarioActualizacion" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaActualizacion", "UsuarioActualizacion" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Categoria",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaActualizacion", "UsuarioActualizacion" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Taller");

            migrationBuilder.DropColumn(
                name: "UsuarioActualizacion",
                table: "Taller");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "UsuarioActualizacion",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Inscripcion");

            migrationBuilder.DropColumn(
                name: "UsuarioActualizacion",
                table: "Inscripcion");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "UsuarioActualizacion",
                table: "Categoria");

            migrationBuilder.DropColumn(
                name: "FechaActualizacion",
                table: "Alumno");

            migrationBuilder.DropColumn(
                name: "UsuarioActualizacion",
                table: "Alumno");
        }
    }
}
