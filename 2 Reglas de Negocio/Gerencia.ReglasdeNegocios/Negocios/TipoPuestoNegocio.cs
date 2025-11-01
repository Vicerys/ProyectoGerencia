using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class TipoPuestoNegocio
    {
        private readonly AppDbContext _context;
        public TipoPuestoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoPuestoDto>> ObtenerTodosAsync()
        {
            List<TipoPuestoDto> puestosDto = new List<TipoPuestoDto>();

            var puestos = await _context.TipoPuestos.ToListAsync();
            
            puestosDto = puestos.Select(p => new TipoPuestoDto
            {
                PuestoId = p.PuestoId,
                Nombre = p.Nombre
            }).ToList();

            return puestosDto;
        }
    }
}
