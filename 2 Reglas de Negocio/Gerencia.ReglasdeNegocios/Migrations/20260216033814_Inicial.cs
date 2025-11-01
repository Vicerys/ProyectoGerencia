using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gerencia.ReglasdeNegocios.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Disponibilidad",
                columns: table => new
                {
                    DisponibilidadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibilidad", x => x.DisponibilidadId);
                });

            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    PuestoId = table.Column<int>(type: "Int", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    //Edad = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "varchar(100)", nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Usuario = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    Contrasena = table.Column<string>(type: "varchar(200)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleado", x => x.EmpleadoId);
                });

            migrationBuilder.CreateTable(
                name: "Equipo",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Ubicacion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    Foto = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Especificaciones = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    DisponibilidadId = table.Column<int>(type: "Int", nullable: false),
                    Mantenimiento = table.Column<int>(type: "Int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipo", x => x.EquipoId);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "Importancia",
                columns: table => new
                {
                    ImportanciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Importancia", x => x.ImportanciaId);
                });

            migrationBuilder.CreateTable(
                name: "Proceso",
                columns: table => new
                {
                    ProcesoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProyectoId = table.Column<int>(type: "Int", nullable: false),
                    ImportanciaId = table.Column<int>(type: "Int", nullable: false),
                    FechaTerminoEstimada = table.Column<DateTime>(type: "datetime", nullable: false),
                    FechaTerminoReal = table.Column<DateTime>(type: "datetime", nullable: false),
                    Entregables = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    EstadoId = table.Column<int>(type: "Int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proceso", x => x.ProcesoId);
                });

            migrationBuilder.CreateTable(
                name: "Proyecto",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    FechaLimite = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proyecto", x => x.ProyectoId);
                });

            migrationBuilder.CreateTable(
                name: "Puesto",
                columns: table => new
                {
                    PuestoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puesto", x => x.PuestoId);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    TareaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ProcesoId = table.Column<int>(type: "Int", nullable: false),
                    EstadoId = table.Column<int>(type: "Int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.TareaId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoProyecto",
                columns: table => new
                {
                    EmpleadoProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpleadoId = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoProyecto", x => x.EmpleadoProyectoId);
                    table.ForeignKey(
                        name: "FK_EmpleadoProyecto_Proyecto_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoProyecto_empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "empleado",
                        principalColumn: "EmpleadoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoProyectoDetalle",
                columns: table => new
                {
                    EmpleadoProyectoId = table.Column<int>(type: "int", nullable: false),
                    Linea = table.Column<int>(type: "int", nullable: false),
                    ProyectoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoProyectoDetalle", x => new { x.EmpleadoProyectoId, x.Linea });
                    table.ForeignKey(
                        name: "FK_EmpleadoProyectoDetalle_EmpleadoProyecto_EmpleadoProyectoId",
                        column: x => x.EmpleadoProyectoId,
                        principalTable: "EmpleadoProyecto",
                        principalColumn: "EmpleadoProyectoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleadoProyectoDetalle_Proyecto_ProyectoID",
                        column: x => x.ProyectoID,
                        principalTable: "Proyecto",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Disponibilidad",
                columns: new[] { "DisponibilidadId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "Ocupado" },
                    { 3, "Mantenimiento" }
                });

            migrationBuilder.InsertData(
                table: "Estado",
                columns: new[] { "EstadoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Incompleto" },
                    { 2, "En curso" },
                    { 3, "Completado" }
                });

            migrationBuilder.InsertData(
                table: "Importancia",
                columns: new[] { "ImportanciaId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Baja" },
                    { 2, "Media" },
                    { 3, "Alta" },
                    { 4, "Urgente" }
                });

            migrationBuilder.InsertData(
                table: "Puesto",
                columns: new[] { "PuestoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Gerente" },
                    { 2, "Análista" },
                    { 3, "Técnico" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyecto_EmpleadoId",
                table: "EmpleadoProyecto",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyecto_ProyectoId",
                table: "EmpleadoProyecto",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoProyectoDetalle_ProyectoID",
                table: "EmpleadoProyectoDetalle",
                column: "ProyectoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disponibilidad");

            migrationBuilder.DropTable(
                name: "EmpleadoProyectoDetalle");

            migrationBuilder.DropTable(
                name: "Equipo");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Importancia");

            migrationBuilder.DropTable(
                name: "Proceso");

            migrationBuilder.DropTable(
                name: "Puesto");

            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "EmpleadoProyecto");

            migrationBuilder.DropTable(
                name: "Proyecto");

            migrationBuilder.DropTable(
                name: "empleado");
        }
    }
}
