using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gerencia.Core.Dtos
{
   public class TareaDto
   {
        [JsonPropertyName("tareaId")]
        [Display(Name = "ID")]
        public int TareaId { get; set; }

        [JsonPropertyName("nombre")]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [JsonPropertyName("procesoId")]
        [Display(Name = "Proceso vinculado")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public int ProcesoId { get; set; }

        [JsonPropertyName("estadoId")]
        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Incompleto, 2-En curso, 3-Completado")]
        public int EstadoId { get; set; }

        [JsonPropertyName("descripcion")]
        [Display(Name = "Descripción de la tarea")]
        public string Descripcion { get; set; }

   
    }
}
