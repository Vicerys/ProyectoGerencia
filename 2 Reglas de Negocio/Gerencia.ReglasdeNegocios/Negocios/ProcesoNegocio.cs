using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    
    public class ProcesoNegocio
    {
        private readonly AppDbContext _context;

        public ProcesoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProcesoDto>> ObtenerTodosAsync()
        {
            List<ProcesoDto> procesosDto = new List<ProcesoDto>();

            var procesos = await _context.Procesos.ToListAsync();

            procesosDto = procesos.Select(p => new ProcesoDto
            {
                ProcesoId = p.ProcesoId,
                Nombre = p.Nombre,
                ProyectoId = p.ProyectoId,
                ImportanciaId = p.ImportanciaId,
                FechaTerminoEstimada = p.FechaTerminoEstimada,
                FechaTerminoReal = p.FechaTerminoReal,
                Entregables = p.Entregables,
                EstadoId = p.EstadoId
            
            }).ToList();

            return procesosDto;
        }
    }
}
