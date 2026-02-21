using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class EquipoDto
    {
        [JsonPropertyName("equipoId")]
        [Display(Name = "ID")]
        public int EquipoId { get; set; }

        [JsonPropertyName("Nombre")]
        [Display(Name = "Nombre del equipo")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [JsonPropertyName("ubicacion")]
        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string Ubicacion { get; set; }

        [JsonPropertyName("foto")]
        [Display(Name = "Foto del equipo")]
        public string? Foto { get; set; }

        [JsonPropertyName("especificaciones")]
        [Display(Name = "Especificaciones técnicas")]
        public string Especificaciones { get; set; }

        [JsonPropertyName("disponibilidadId")]
        [Display(Name = "Disponibilidad")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Disponible, 2-Ocupado, 3-Mantenimiento")]
        public int DisponibilidadId { get; set; }

        [JsonPropertyName("mantenimiento")]
        [Display(Name = "Frecuencia de mantenimiento en días")]
        public int Mantenimiento { get; set; }

    }
}
