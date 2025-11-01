using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models
{
    public class EmpleadoProyectoDetalle
    {
        [Display(Name = "ID de Asignacion")]
        public int EmpleadoProyectoId { get; set; }

        [Display(Name = "Línea")]
        public int Linea { get; set; } // autonumerado por pedido

        [Display(Name = "ID de Proyecto")]
        public int ProyectoID { get; set; }

        
        public EmpleadoProyecto? EmpleadoProyecto { get; set; }
    }
}
