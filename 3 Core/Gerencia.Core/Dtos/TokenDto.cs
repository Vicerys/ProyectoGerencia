using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Core.Dtos
{
    public class TokenDto
    {
        public String token { get; set; }
        public DateTime fechaExpiracion { get; set; }
    }
}
