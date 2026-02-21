using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gerencia.Core.Dtos
{
   public class EmpleadoDtoIn
   {
        [JsonPropertyName("nombre")]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }
        
        [JsonPropertyName("puestoId")]
        [Display(Name = "Tipo de empleado")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Gerente, 2-Análista, 3-Técnico")]
        public int PuestoId { get; set; }
        
        [JsonPropertyName("fechaNacimiento")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [JsonPropertyName("ubicacion")]
        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string Ubicacion { get; set; }

        [JsonPropertyName("foto")]
        [Display(Name = "Foto del empleado")]
        public string? Foto { get; set; }

        [JsonPropertyName("usuario")]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(50)]
        public string Usuario { get; set; }

        [JsonPropertyName("contrasena")]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(50)]
        public string Contrasena { get; set; }
    }
}
