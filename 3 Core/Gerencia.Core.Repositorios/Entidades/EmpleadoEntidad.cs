using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    // Clase abstracta que representa un empleado, va a ser heredada por otras clases específicas de empleados dependiendo del puesto
    //public abstract class Empleado
    public class EmpleadoEntidad
    {
        [Display(Name = "ID")]
        public int EmpleadoId { get; set; }
        
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }
        
        [Display(Name ="Tipo de empleado")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        [Range(1, 3, ErrorMessage = "1-Gerente, 2-Análista, 3-Técnico")]
        public int PuestoId { get; set; }
        
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        
        [Display(Name = "Edad")]
        public int Edad { get; set; }
        
        [Display(Name = "Ubicación")]
        [Required(ErrorMessage = "{0} es obligatorio")]
        public string Ubicacion { get; set; }

        [Display(Name = "Foto del empleado")]
        public string? Foto { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(50)]
        public string Usuario { get; set; }
        
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(50)]
        public string Contrasena { get; set; }
    }
}
