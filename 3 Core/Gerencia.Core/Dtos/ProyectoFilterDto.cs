using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class ProyectoFilterDto
    {
        public int? ProyectoID { get; set; }
        public string? NombreLike { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; } = 0;
    }
}
