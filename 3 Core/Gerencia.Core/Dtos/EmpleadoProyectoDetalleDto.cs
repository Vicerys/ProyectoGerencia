using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Dtos
{
    public class EmpleadoProyectoDetalleDto
    {
        [Display(Name = "ID de Asignacion")]
        public int EmpleadoProyectoId { get; set; }

        [Display(Name = "Línea")]
        public int Linea { get; set; } // autonumerado por asignación

        [Display(Name = "ID de Proyecto")]
        public int ProyectoID { get; set; }

        public EmpleadoProyectoDto? EmpleadoProyecto { get; set; }

    }
}