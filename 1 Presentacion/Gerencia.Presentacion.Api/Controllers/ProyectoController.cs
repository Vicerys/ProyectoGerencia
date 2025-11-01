using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly ProyectoNegocio _proyectoNegocio;

        public ProyectoController(ProyectoNegocio proyectoNegocio)
        {
            _proyectoNegocio = proyectoNegocio;
        }
        [HttpGet]
        public async Task<IActionResult> GetProyectos()
        {

        List<ProyectoDto> proyectos;
            proyectos = await _proyectoNegocio.ObtenerTodosAsync();
            return Ok(proyectos);
        }

    }
}
