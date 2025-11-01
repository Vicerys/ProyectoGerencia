using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Dtos
{
   public class ProcesoDto
   {

        [Display(Name = "ID")]
        public int ProcesoId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Proyecto enlazado")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int ProyectoId { get; set; }

        [Display(Name = "Importancia")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 4, ErrorMessage = "1-Baja, 2-Media, 3-Alta, 4-Urgente")]
        public int ImportanciaId { get; set; }

        [Display(Name = "Fecha de termino estimada")]
        [DataType(DataType.Date)]
        public DateTime FechaTerminoEstimada { get; set; }

        [Display(Name = "Fecha de termino real")]
        [DataType(DataType.Date)]
        public DateTime? FechaTerminoReal { get; set; }

        [Display(Name = "Entregables")]
        public string Entregables { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Incompleto, 2-En curso, 3-Completado")]
        public int EstadoId { get; set; }

    }
}
