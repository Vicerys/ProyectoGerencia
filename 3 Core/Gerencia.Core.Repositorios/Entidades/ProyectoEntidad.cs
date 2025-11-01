using System.ComponentModel.DataAnnotations;

namespace Gerencia.Core.Repositorios.Entidades
{
    public class ProyectoEntidad
    {
        [Display(Name = "ID")]
        public int ProyectoId { get; set; }
        
        [Display(Name = "Nombre del proyecto")]
        [Required(ErrorMessage = "{0} es obligatorio"), StringLength(100)]
        public string Nombre { get; set; }
        
        
        [Display(Name = "Fecha límite de entrega")]
        [DataType(DataType.Date)]
        public DateTime FechaLimite { get; set; }
    }
}
