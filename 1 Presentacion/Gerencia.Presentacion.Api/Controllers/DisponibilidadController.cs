using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public DisponibilidadController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposPuesto()
        {
            List<DisponibilidadDto> disponibilidades;
            disponibilidades = await _unityofWork.DisponibilidadNegocio.ObtenerTodosAsync();

            return Ok(disponibilidades);
        }
    }
}
