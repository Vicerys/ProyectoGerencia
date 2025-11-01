using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Mvc;

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
