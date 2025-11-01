using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoProyectoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;
        public EmpleadoProyectoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmpleadoProyecto()
        {
            List<EmpleadoProyectoDto> empleadosproyectos;
            empleadosproyectos = await _unityofWork.EmpleadoProyectoNegocio.ObtenerTodosAsync();
            return Ok(empleadosproyectos);
        }
    }
}
