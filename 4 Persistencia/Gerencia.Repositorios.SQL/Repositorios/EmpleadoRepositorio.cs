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
    public class EmpleadoRepositorio
    {
        private readonly AppDbContext _context;

        public EmpleadoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmpleadoEntidad> ObtenerTodosAsync(string usuario)
        {
            EmpleadoEntidad empleado;
            empleado = await _context.Empleados.Where(x => x.Usuario == usuario).FirstOrDefaultAsync();
            return empleado;

            //return await Task.Run(() => _context.Empleados.ToList());
        }

    }
}
