using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Dtos
{
    public class ApartadoDto 
    {
        //Clase intermedia para la relación muchos a muchos entre Empleado y Equipo que representa el apartado de empleados a equipos

        public int ApartadoId { get; set; }

        [Display(Name = "ID de Empleado")]
        public int EmpleadoId { get; set; }

        public EmpleadoDto? Empleado { get; set; } // Propiedad de navegación para acceder a los detalles del empleado

        [Display(Name = "ID del Equipo")]
        public int EquipoId { get; set; }

        public EquipoDto? Equipo { get; set; } // Propiedad de navegación para acceder a los detalles del equipo

        [Display(Name = "Fecha/hora de Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Fecha/hora de Fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        public ICollection<ApartadoDetalleDto> Detalles { get; set; } = new List<ApartadoDetalleDto>();


    }
}
