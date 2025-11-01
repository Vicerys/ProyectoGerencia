using System.ComponentModel.DataAnnotations;

namespace Gerencia.Presentacion.MVC.Models
{
    public class EmpleadoProyecto
    {
        //Clase intermedia para la relación muchos a muchos entre Empleado y Proyecto que representa la asignación de empleados a proyectos
        [Key]
        public int EmpleadoProyectoId { get; set; }
        
        [Display(Name = "ID de Empleado")]
        public int EmpleadoId { get; set; }

        public Empleado? Empleado { get; set; } // Propiedad de navegación para acceder a los detalles del empleado

        [Display(Name = "ID de Proyecto")]
        public int ProyectoId { get; set; }

        public Proyecto? Proyecto { get; set; } // Propiedad de navegación para acceder a los detalles del proyecto

        [Display(Name = "Fecha de Asignacion")]
        [DataType(DataType.Date)]
        public DateTime FechaAsignacion { get; set; }

        public ICollection<EmpleadoProyectoDetalle> Detalles { get; set; } = new List<EmpleadoProyectoDetalle>();

    }
}
