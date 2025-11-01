using Gerencia.Core.Dtos;
using Gerencia.Core.Repositorios.Entidades;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;
using Microsoft.CodeAnalysis;
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

        public async Task<PaginatedList<ProyectoDto>> FiltrarAsync(ProyectoFilterDto filtro)
        {
            // Consulta base
            IQueryable<ProyectoEntidad> query = _context.Proyectos.AsNoTracking();

            // Aplicar filtros dinámicos
            if (filtro.ProyectoID.HasValue)
                query = query.Where(p => p.ProyectoId == filtro.ProyectoID.Value);

            if (!string.IsNullOrWhiteSpace(filtro.NombreLike))
                query = query.Where(p => EF.Functions.Like(p.Nombre, $"%{filtro.NombreLike}%"));

            query = query.OrderBy(p => p.ProyectoId);

            // Map a DTO
            var queryDto = query.Select(p => new ProyectoDto
            {
                ProyectoId = p.ProyectoId,
                Nombre = p.Nombre
            });

            // Paginación usando PaginatedList
            var paged = await PaginatedList<ProyectoDto>.CreateAsync(
                queryDto, filtro.Page, filtro.PageSize);

            return paged;
        }

    }
}
