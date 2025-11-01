using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Dtos
{
   public class TareaDto
   {

        [Display(Name = "ID")]
        public int TareaId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Proceso vinculado")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int ProcesoId { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Incompleto, 2-En curso, 3-Completado")]
        public int EstadoId { get; set; }

        [Display(Name = "Descripción de la tarea")]
        public string Descripcion { get; set; }

   
    }
}
