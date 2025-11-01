using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class EmpleadoProyectoNegocio
    {
        private readonly AppDbContext _context;

        public EmpleadoProyectoNegocio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpleadoProyectoDto>> ObtenerTodosAsync()
        {
            List<EmpleadoProyectoDto> empleadosproyectosDto = new List<EmpleadoProyectoDto>();

            var empleadosproyectos = await _context.EmpleadoProyectos.ToListAsync();

            empleadosproyectosDto = empleadosproyectos.Select(p => new EmpleadoProyectoDto
            {
                EmpleadoProyectoId = p.EmpleadoProyectoId,
                EmpleadoId = p.EmpleadoId,
                ProyectoId = p.ProyectoId,
                FechaAsignacion = p.FechaAsignacion
            }).ToList();

            return empleadosproyectosDto;
        }
    }
}
