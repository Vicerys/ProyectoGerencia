using Gerencia.Core.Repositorios.Entidades;
using Gerencia.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Repositorios.SQL.Repositorios
{
    public class ProyectoRepositorio
    {
        private readonly AppDbContext _context;
        ProyectoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProyectoEntidad>> ObtenerTodosAsync()
        {
            List<ProyectoEntidad> proyecto = await _context.Proyectos.ToListAsync();

            return proyecto;
        }
    }
}
