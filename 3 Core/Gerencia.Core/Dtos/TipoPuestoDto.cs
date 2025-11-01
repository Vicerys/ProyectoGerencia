using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class TipoPuestoDto
    {

        public int PuestoId { get; set; }
        [Required, StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

    }
}
