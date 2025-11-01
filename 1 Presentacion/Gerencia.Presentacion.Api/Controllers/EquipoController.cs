using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public EquipoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetEquipos()
        {

            List<EquipoDto> equipos;
            equipos = await _unityofWork.EquipoNegocio.ObtenerTodosAsync();
            return Ok(equipos);
        }
    }
}
