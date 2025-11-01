using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    public class EmpleadoProyectoEntidad
    {
        //Clase intermedia para la relación muchos a muchos entre Empleado y Proyecto que representa la asignación de empleados a proyectos
        [Key]
        public int EmpleadoProyectoId { get; set; }
        
        [Display(Name = "ID de Empleado")]
        public int EmpleadoId { get; set; }

        public EmpleadoEntidad? Empleado { get; set; } // Propiedad de navegación para acceder a los detalles del empleado

        [Display(Name = "ID de Proyecto")]
        public int ProyectoId { get; set; }

        public ProyectoEntidad? Proyecto { get; set; } // Propiedad de navegación para acceder a los detalles del proyecto

        [Display(Name = "Fecha de Asignacion")]
        [DataType(DataType.Date)]
        public DateTime FechaAsignacion { get; set; }

        public ICollection<EmpleadoProyectoDetalleEntidad> Detalles { get; set; } = new List<EmpleadoProyectoDetalleEntidad>();

    }
}
