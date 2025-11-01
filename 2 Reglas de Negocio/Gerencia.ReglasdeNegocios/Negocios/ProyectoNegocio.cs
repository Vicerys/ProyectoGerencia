using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    
    public class ProyectoNegocio
    {
        private readonly AppDbContext _context;

        public ProyectoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProyectoDto>> ObtenerTodosAsync()
        {
            List<ProyectoDto> proyectosDto = new List<ProyectoDto>();

            var proyectos = await _context.Proyectos.ToListAsync();

            proyectosDto = proyectos.Select(p => new ProyectoDto
            {
                ProyectoId = p.ProyectoId,
                Nombre = p.Nombre,
                FechaLimite = p.FechaLimite
            }).ToList();

            return proyectosDto;
        }
    }
}
