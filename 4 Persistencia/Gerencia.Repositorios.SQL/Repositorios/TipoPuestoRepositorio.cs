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
    public class TipoPuestoRepositorio
    {
        readonly AppDbContext _context;
        public TipoPuestoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoPuesto>> ObtenerTodosAsync()
        {
            List<TipoPuesto> puestos = await _context.TipoPuestos.ToListAsync();

            return puestos;
        }
    }
}
