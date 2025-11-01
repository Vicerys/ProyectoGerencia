using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.ReglasdeNegocios.Negocios
{

    public class TareaNegocio
    {
        private readonly AppDbContext _context;

        public TareaNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TareaDto>> ObtenerTodosAsync()
        {
            List<TareaDto> tareasDto = new List<TareaDto>();

            var tareas = await _context.Tareas.ToListAsync();

            tareasDto = tareas.Select(p => new TareaDto
            {
                TareaId = p.TareaId,
                Nombre = p.Nombre,
                ProcesoId = p.ProcesoId,
                EstadoId = p.EstadoId,
                Descripcion = p.Descripcion

            }).ToList();

            return tareasDto;
        }
        public async Task<TareaDto> ObtenerPorIdAsync(int tareaId)
        {
            TareaDto tareaDto = new TareaDto();
            var tarea = await _context.Tareas.FirstOrDefaultAsync(p => p.TareaId == tareaId);
            if (tarea != null)
            {
                tareaDto = new TareaDto
                {
                    TareaId = tarea.TareaId,
                    Nombre = tarea.Nombre,
                    ProcesoId = tarea.ProcesoId,
                    EstadoId = tarea.EstadoId,
                    Descripcion = tarea.Descripcion
                };
            }
            return tareaDto;
        }
    }
}
