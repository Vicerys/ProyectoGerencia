using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    public class EquipoEntidad
    {
        [Display(Name = "ID")]
        public int EquipoId { get; set; }

        [Display(Name = "Nombre del equipo")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Ubicacion { get; set; }

        [Display(Name = "Foto del equipo")]
        public string? Foto { get; set; }

        [Display(Name = "Especificaciones técnicas")]
        public string Especificaciones { get; set; }

        [Display(Name = "Disponibilidad")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Disponible, 2-Ocupado, 3-Mantenimiento")]
        public int DisponibilidadId { get; set; }

        [Display(Name = "Frecuencia de mantenimiento en días")]
        public int Mantenimiento { get; set; }
    }
}
