using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class ProyectoDto
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
