using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerencia.Repositorios.SQL.Repositorios
{
    public class Repositorio
    {
        public EmpleadoRepositorio Empleado { get; set; }
        public TipoPuestoRepositorio TipoPuesto { get; set; }

        public Repositorio(EmpleadoRepositorio empleado, TipoPuestoRepositorio tipoPuesto)
        {
            
            TipoPuesto = tipoPuesto;
        }
    }

    public class  RepositorioProyecto
    {
        public EmpleadoRepositorio Empleado { get; set; }
        public ProyectoRepositorio Proyecto { get; set; }

        public RepositorioProyecto(EmpleadoRepositorio empleado, ProyectoRepositorio proyecto)
        {
            Empleado = empleado;
            Proyecto = proyecto;
        }
    }

    public async Task<TokenDto> ObtenerTokenAsync(string usuario, string contrasena)
    {
        // Lógica para obtener el token de autenticación
        var empleado = await _context.Empleado
            .ObtenerEmpleadoAsync(inicioSesionDto.Usuario);

            if (empleado == null || empleado.Contrasena != inicioSesionDto.Contrasena)
            {
                throw new UnauthorizedAccessException("Credenciales inválidas.");
            }
        }
}
