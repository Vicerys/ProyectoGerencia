using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class EstadoNegocio
    {
        private readonly AppDbContext _context;
        public EstadoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EstadoDto>> ObtenerTodosAsync()
        {
            List<EstadoDto> estadosDto = new List<EstadoDto>();

            var estados = await _context.Estados.ToListAsync();

            estadosDto = estados.Select(p => new EstadoDto
            {
                EstadoId = p.EstadoId,
                Nombre = p.Nombre
            }).ToList();

            return estadosDto;
        }


    }
}
