using AutorizacionJwtServicio;
using Gerencia.Core.Dtos;
using Gerencia.Core.Repositorios.Entidades;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.EntityFrameworkCore;
namespace Gerencia.ReglasdeNegocios.Negocios
{
    public class EmpleadoNegocio
    {
        private readonly AppDbContext _context;
        private readonly TokenServicio _tokenServicio;

        public EmpleadoNegocio(AppDbContext context, TokenServicio tokenServicio)
        {
            _context = context;
            _tokenServicio = tokenServicio;

        }

        public async Task<List<EmpleadoDto>> ObtenerTodosAsync()
        {
            List<EmpleadoDto> empleadosDto = new List<EmpleadoDto>();

            var empleados = await _context.Empleados.ToListAsync();

            empleadosDto = empleados.Select(p => new EmpleadoDto
            {
                EmpleadoId = p.EmpleadoId,
                Nombre = p.Nombre,
                PuestoId=p.PuestoId,
                FechaNacimiento=p.FechaNacimiento,
                Edad=p.Edad,
                Ubicacion=p.Ubicacion,
                Foto=p.Foto,
                Usuario=p.Usuario,
                //Contrasena=p.Contrasena

            }).ToList();

            return empleadosDto;

        }

        public async Task<EmpleadoDto> ObtenerTodosAsync(string usuario)
        {
            EmpleadoDto empleado;
            EmpleadoEntidad empleadoEntidad = await _context.Empleados.Where(x => x.Usuario == usuario).FirstOrDefaultAsync();
            
            empleado = new EmpleadoDto
            {
                EmpleadoId = empleadoEntidad.EmpleadoId,
                Nombre = empleadoEntidad.Nombre,
                PuestoId = empleadoEntidad.PuestoId,
                FechaNacimiento = empleadoEntidad.FechaNacimiento,
                Edad = empleadoEntidad.Edad,
                Ubicacion = empleadoEntidad.Ubicacion,
                Foto = empleadoEntidad.Foto,
                Usuario = empleadoEntidad.Usuario
                //Contrasena = empleadoEntidad.Contrasena
            };

            return empleado;


            //return await Task.Run(() => _context.Empleados.ToList());
        }

        public async Task<TokenDto> ObtenerTokenAsync(InicioSesionDto inicioSesion)
        {
           
            var empleado = await _context.Empleados.Where(x => x.Usuario == inicioSesion.usuario).FirstOrDefaultAsync();
            if (empleado != null)
            {
                if (empleado.Contrasena != inicioSesion.contrasena)
                {
                    return null;
                }
                else
                {

                    String token = _tokenServicio.ObtenerToken(empleado.Nombre, "empleado", empleado.EmpleadoId.ToString(), "", DateTime.Now.AddMinutes(15));
                    TokenDto tokenDto = new TokenDto
                    {
                        token = token, fechaExpiracion = DateTime.Now.AddMinutes(15)

                    };
                    return tokenDto;
                }
            } return null;
        }
    }
}
