using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public ProyectoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetProyectos()
        {

            List<ProyectoDto> proyectos;
            proyectos = await _unityofWork.ProyectoNegocio.ObtenerTodosAsync();
            return Ok(proyectos);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Filter(ProyectoFilterDto filtro)
        {
            var resultado = await _unityofWork
                .ProyectoNegocio
                .FiltrarAsync(filtro);

            return Ok(resultado);
        }

    }
}
