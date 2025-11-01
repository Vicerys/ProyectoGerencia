using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class EmpleadoProyectoDto
    {
        //Clase intermedia para la relación muchos a muchos entre Empleado y Proyecto que representa la asignación de empleados a proyectos
        
        public int EmpleadoProyectoId { get; set; }

        [Display(Name = "ID de Empleado")]
        public int EmpleadoId { get; set; }

        public EmpleadoDto? Empleado { get; set; } // Propiedad de navegación para acceder a los detalles del empleado

        [Display(Name = "ID de Proyecto")]
        public int ProyectoId { get; set; }

        public ProyectoDto? Proyecto { get; set; } // Propiedad de navegación para acceder a los detalles del proyecto

        [Display(Name = "Fecha de Asignacion")]
        [DataType(DataType.Date)]
        public DateTime FechaAsignacion { get; set; }

        public ICollection<EmpleadoProyectoDetalleDto> Detalles { get; set; } = new List<EmpleadoProyectoDetalleDto>();


    }
}
