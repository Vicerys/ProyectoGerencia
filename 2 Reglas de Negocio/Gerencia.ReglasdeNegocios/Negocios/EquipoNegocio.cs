using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    
    public class EquipoNegocio
    {
        private readonly AppDbContext _context;

        public EquipoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EquipoDto>> ObtenerTodosAsync()
        {
            List<EquipoDto> equiposDto = new List<EquipoDto>();

            var equipos = await _context.Equipos.ToListAsync();

            equiposDto = equipos.Select(p => new EquipoDto
            {
                EquipoId = p.EquipoId,
                Nombre = p.Nombre,
                Ubicacion = p.Ubicacion,
                Foto = p.Foto,
                Especificaciones = p.Especificaciones,
                DisponibilidadId = p.DisponibilidadId,
                Mantenimiento = p.Mantenimiento

            }).ToList();

            return equiposDto;
        }
    }
}
