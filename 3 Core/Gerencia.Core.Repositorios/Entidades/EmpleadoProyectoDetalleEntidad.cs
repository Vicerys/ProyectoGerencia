using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    public class EmpleadoProyectoDetalleEntidad
    {
        [Display(Name = "ID de Asignacion")]
        public int EmpleadoProyectoId { get; set; }

        [Display(Name = "Línea")]
        public int Linea { get; set; } // autonumerado por pedido

        [Display(Name = "ID de Proyecto")]
        public int ProyectoID { get; set; }

        public EmpleadoProyectoEntidad? EmpleadoProyecto { get; set; }
    }
}
