using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;
        public EmpleadoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmpleados()
        {
            List<EmpleadoDto> empleados;
            empleados = await _unityofWork.EmpleadoNegocio.ObtenerTodosAsync();
            return Ok(empleados);
        }

        [HttpGet("{empleadoId}")]
        public async Task<IActionResult> GetEmpleadoPorId(int empleadoId)
        {
            EmpleadoDto empleado;
            empleado = await _unityofWork.EmpleadoNegocio.ObtenerPorIdAsync(empleadoId);
            if (empleado == null)
            {
                return NotFound("Empleado no encontrado");
            }
            else
            {
                return Ok(empleado);
            }
        }
        

        [HttpPost("AgregarEmpleado")]
        public async Task<IActionResult> AddEmpleado(EmpleadoDtoIn empleadoDto)
        {
            int empleadoCreado;
            empleadoCreado = await _unityofWork.EmpleadoNegocio.AgregarEmpleadoAsync(empleadoDto);
            return Created("", empleadoCreado);
        }

        [HttpPut("ActualizarEmpleado")]
        public async Task<IActionResult> UpdateEmpleado(EmpleadoDto empleadoDto)
        {
            EmpleadoDto empleadoActualizado;
            empleadoActualizado = await _unityofWork.EmpleadoNegocio.ActualizarEmpleadoAsync(empleadoDto);
            if (empleadoActualizado == null)
            {
                return NotFound("Empleado no encontrado");
            }
            else
            {
                return Ok(empleadoActualizado);
            }
        }

        [HttpDelete("EliminarEmpleado")]
        public async Task<IActionResult> DeleteEmpleado(int empleadoId)
        {
            bool eliminado = await _unityofWork.EmpleadoNegocio.EliminarEmpleadoAsync(empleadoId);
            if (eliminado)
            {
                return Ok("Empleado eliminado correctamente");
            }
            else
            {
                return NotFound("Empleado no encontrado");
            }
        }

        [HttpPost("IniciodeSesiones")]
        public async Task<IActionResult> GetToken(InicioSesionDto inicioSesionDto)
        {
            TokenDto token;
            token = await _unityofWork.EmpleadoNegocio.ObtenerTokenAsync(inicioSesionDto);
            if (token == null)
            {
                return NotFound("Datos incorrectos");
            }
            else
            {
                return Ok(token);
            }
        }

    }
}
