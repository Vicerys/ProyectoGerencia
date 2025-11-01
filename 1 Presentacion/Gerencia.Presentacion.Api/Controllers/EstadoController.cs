using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public EstadoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposPuesto()
        {
            List<EstadoDto> estados;
            estados = await _unityofWork.EstadoNegocio.ObtenerTodosAsync();

            return Ok(estados);
        }

    }
}
