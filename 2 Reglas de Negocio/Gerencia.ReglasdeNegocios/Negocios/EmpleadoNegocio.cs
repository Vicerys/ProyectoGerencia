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

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            var hoy = DateTime.Today;
            var edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;

            return edad;
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
                Edad = CalcularEdad(p.FechaNacimiento),
                Ubicacion =p.Ubicacion,
                Foto=p.Foto,
                Usuario=p.Usuario,
                //Contrasena=p.Contrasena

            }).ToList();

            return empleadosDto;

        }

        public async Task<EmpleadoDto> ObtenerPorIdAsync(int empleadoId)
        {
            var empleadoEntidad = await _context.Empleados.FindAsync(empleadoId);
            if (empleadoEntidad == null)
            {
                return null;
            }
            EmpleadoDto empleadoDto = new EmpleadoDto
            {
                EmpleadoId = empleadoEntidad.EmpleadoId,
                Nombre = empleadoEntidad.Nombre,
                PuestoId = empleadoEntidad.PuestoId,
                FechaNacimiento = empleadoEntidad.FechaNacimiento,
                Edad = CalcularEdad(empleadoEntidad.FechaNacimiento),
                Ubicacion = empleadoEntidad.Ubicacion,
                Foto = empleadoEntidad.Foto,
                Usuario = empleadoEntidad.Usuario
                //Contrasena = empleadoEntidad.Contrasena
            };
            return empleadoDto;
        }

        public async Task<int> AgregarEmpleadoAsync(EmpleadoDtoIn empleadoDto)
        {
            EmpleadoEntidad empleadoEntidad = new EmpleadoEntidad
            {
                Nombre = empleadoDto.Nombre,
                PuestoId = empleadoDto.PuestoId,
                FechaNacimiento = empleadoDto.FechaNacimiento,
                Ubicacion = empleadoDto.Ubicacion,
                Foto = empleadoDto.Foto,
                Usuario = empleadoDto.Usuario,
                Contrasena = empleadoDto.Contrasena
            };
            _context.Empleados.Add(empleadoEntidad);
            await  _context.SaveChangesAsync();
            int empleadoId = empleadoEntidad.EmpleadoId;
            return empleadoId;
        }

        public async Task<EmpleadoDto> ActualizarEmpleadoAsync(EmpleadoDto empleadoDto)
        {
            var empleadoEntidad = await _context.Empleados.FindAsync(empleadoDto.EmpleadoId);
            if (empleadoEntidad == null)
            {
                return null;
            }
            empleadoEntidad.Nombre = empleadoDto.Nombre;
            empleadoEntidad.PuestoId = empleadoDto.PuestoId;
            empleadoEntidad.FechaNacimiento = empleadoDto.FechaNacimiento;
            empleadoEntidad.Ubicacion = empleadoDto.Ubicacion;
            empleadoEntidad.Foto = empleadoDto.Foto;
            empleadoEntidad.Usuario = empleadoDto.Usuario;
            //empleadoEntidad.Contrasena = empleadoDto.Contrasena;
            await _context.SaveChangesAsync();
            return empleadoDto;
        }

        public async Task<bool> EliminarEmpleadoAsync(int empleadoId)
        {
            var empleadoEntidad = await _context.Empleados.FindAsync(empleadoId);
            if (empleadoEntidad == null)
            {
                return false;
            }
            _context.Empleados.Remove(empleadoEntidad);
            await _context.SaveChangesAsync();
            return true;
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
                Ubicacion = empleadoEntidad.Ubicacion,
                Foto = empleadoEntidad.Foto,
                Usuario = empleadoEntidad.Usuario
                //Contrasena = empleadoEntidad.Contrasena
            };

            return empleado;


            //return await Task.Run(() => _context.Empleados.ToList());
        }

        public async Task<List<EmpleadoDto>> ObtenerEmpleadosPorPuestoAsync(int puestoId)
        {
            List<EmpleadoDto> empleadosDto = new List<EmpleadoDto>();
            var empleados = await _context.Empleados.Where(e => e.PuestoId == puestoId).ToListAsync();
            empleadosDto = empleados.Select(p => new EmpleadoDto
            {
                EmpleadoId = p.EmpleadoId,
                Nombre = p.Nombre,
                PuestoId = p.PuestoId,
                FechaNacimiento = p.FechaNacimiento,
                Ubicacion = p.Ubicacion,
                Foto = p.Foto,
                Usuario = p.Usuario
                //Contrasena = p.Contrasena
            }).ToList();
            return empleadosDto;
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

                    String token = _tokenServicio.ObtenerToken(empleado.Nombre, empleado.PuestoId.ToString(), empleado.EmpleadoId.ToString(), DateTime.Now.AddMinutes(15));
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
