using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Dtos
{
    public class ApartadoDetalleDto
    {
        [Display(Name = "ID de Asignacion")]
        public int ApartadoId { get; set; }

        [Display(Name = "Línea")]
        public int Linea { get; set; } // autonumerado por asignación

        [Display(Name = "ID de Proyecto")]
        public int EquipoID { get; set; }

        public ApartadoDto? Apartado { get; set; }

    }
}