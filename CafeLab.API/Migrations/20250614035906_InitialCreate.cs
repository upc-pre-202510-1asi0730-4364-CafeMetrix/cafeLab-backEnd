using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CafeLab.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CostosLote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Lote = table.Column<string>(type: "longtext", nullable: false),
                    MateriaPrima = table.Column<string>(type: "longtext", nullable: false),
                    ManoObra = table.Column<string>(type: "longtext", nullable: false),
                    Transporte = table.Column<string>(type: "longtext", nullable: false),
                    Almacenamiento = table.Column<string>(type: "longtext", nullable: false),
                    Procesamiento = table.Column<int>(type: "int", nullable: false),
                    OtrosCostos = table.Column<int>(type: "int", nullable: false),
                    Totales_TotalLote = table.Column<double>(type: "double", nullable: false),
                    Totales_CostPerKg = table.Column<double>(type: "double", nullable: false),
                    Totales_CostPerCup = table.Column<double>(type: "double", nullable: false),
                    Detalle_CostoKgCafeVerde = table.Column<double>(type: "double", nullable: false),
                    Detalle_CantidadCafeVerde = table.Column<double>(type: "double", nullable: false),
                    Detalle_HorasTrabajadas = table.Column<int>(type: "int", nullable: false),
                    Detalle_CostoPorHora = table.Column<double>(type: "double", nullable: false),
                    Detalle_NumeroTrabajadores = table.Column<int>(type: "int", nullable: false),
                    Detalle_CostoTransporteKgCafeVerde = table.Column<double>(type: "double", nullable: false),
                    Detalle_CantidadTransporteCafeVerde = table.Column<int>(type: "int", nullable: false),
                    Detalle_EnergiaElectrica = table.Column<int>(type: "int", nullable: false),
                    Detalle_MantenimientoMaquinaria = table.Column<int>(type: "int", nullable: false),
                    Detalle_InsumosProcesamiento = table.Column<int>(type: "int", nullable: false),
                    Detalle_AguaUtilizada = table.Column<int>(type: "int", nullable: false),
                    Detalle_DepreciacionEquipos = table.Column<int>(type: "int", nullable: false),
                    Detalle_DiasAlmacen = table.Column<int>(type: "int", nullable: false),
                    Detalle_CostoDiarioAlmacen = table.Column<int>(type: "int", nullable: false),
                    Detalle_ControlCalidad = table.Column<int>(type: "int", nullable: false),
                    Detalle_Certificaciones = table.Column<int>(type: "int", nullable: false),
                    Detalle_Seguros = table.Column<int>(type: "int", nullable: false),
                    Detalle_GastosAdministrativos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostosLote", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CuppingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Origin = table.Column<string>(type: "longtext", nullable: false),
                    Variety = table.Column<string>(type: "longtext", nullable: false),
                    Process = table.Column<string>(type: "longtext", nullable: false),
                    Lot = table.Column<string>(type: "longtext", nullable: false),
                    Profile = table.Column<string>(type: "longtext", nullable: false),
                    Ratings_Fragancia = table.Column<int>(type: "int", nullable: false),
                    Ratings_Sabor = table.Column<int>(type: "int", nullable: false),
                    Ratings_Acidez = table.Column<int>(type: "int", nullable: false),
                    Ratings_Cuerpo = table.Column<int>(type: "int", nullable: false),
                    Ratings_Balance = table.Column<int>(type: "int", nullable: false),
                    Ratings_Postgusto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuppingSessions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MovimientosInventario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Lote = table.Column<string>(type: "longtext", nullable: false),
                    Producto = table.Column<string>(type: "longtext", nullable: false),
                    Cantidad = table.Column<string>(type: "longtext", nullable: false),
                    TipoCafe = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosInventario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostosLote");

            migrationBuilder.DropTable(
                name: "CuppingSessions");

            migrationBuilder.DropTable(
                name: "MovimientosInventario");
        }
    }
}
