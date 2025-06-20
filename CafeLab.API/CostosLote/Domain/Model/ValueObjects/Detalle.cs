namespace CafeLab.API.CostosLote.Domain.Model.ValueObjects;

public class Detalle
{
    public double CostoKgCafeVerde { get; set; }
    public double CantidadCafeVerde { get; set; }
    public int HorasTrabajadas { get; set; }
    public double CostoPorHora { get; set; }
    public int NumeroTrabajadores { get; set; }
    public double CostoTransporteKgCafeVerde { get; set; }
    public int CantidadTransporteCafeVerde { get; set; }
    public int EnergiaElectrica { get; set; }
    public int MantenimientoMaquinaria { get; set; }
    public int InsumosProcesamiento { get; set; }
    public int AguaUtilizada { get; set; }
    public int DepreciacionEquipos { get; set; }
    public int DiasAlmacen { get; set; }
    public int CostoDiarioAlmacen { get; set; }
    public int ControlCalidad { get; set; }
    public int Certificaciones { get; set; }
    public int Seguros { get; set; }
    public int GastosAdministrativos { get; set; }

    public Detalle(
        double costoKgCafeVerde, double cantidadCafeVerde, int horasTrabajadas, 
        double costoPorHora, int numeroTrabajadores, double costoTransporteKgCafeVerde, 
        int cantidadTransporteCafeVerde, int energiaElectrica, int mantenimientoMaquinaria, 
        int insumosProcesamiento, int aguaUtilizada, int depreciacionEquipos, 
        int diasAlmacen, int costoDiarioAlmacen, int controlCalidad, 
        int certificaciones, int seguros, int gastosAdministrativos)
    {
        CostoKgCafeVerde = costoKgCafeVerde;
        CantidadCafeVerde = cantidadCafeVerde;
        HorasTrabajadas = horasTrabajadas;
        CostoPorHora = costoPorHora;
        NumeroTrabajadores = numeroTrabajadores;
        CostoTransporteKgCafeVerde = costoTransporteKgCafeVerde;
        CantidadTransporteCafeVerde = cantidadTransporteCafeVerde;
        EnergiaElectrica = energiaElectrica;
        MantenimientoMaquinaria = mantenimientoMaquinaria;
        InsumosProcesamiento = insumosProcesamiento;
        AguaUtilizada = aguaUtilizada;
        DepreciacionEquipos = depreciacionEquipos;
        DiasAlmacen = diasAlmacen;
        CostoDiarioAlmacen = costoDiarioAlmacen;
        ControlCalidad = controlCalidad;
        Certificaciones = certificaciones;
        Seguros = seguros;
        GastosAdministrativos = gastosAdministrativos;
    }

    // Constructor sin parámetros para Entity Framework Core
    public Detalle() { }
} 