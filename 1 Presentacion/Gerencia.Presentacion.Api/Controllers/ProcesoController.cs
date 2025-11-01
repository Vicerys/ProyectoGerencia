using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcesoController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public ProcesoController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetProcesos()
        {

            List<ProcesoDto> procesos;
            procesos = await _unityofWork.ProcesoNegocio.ObtenerTodosAsync();
            return Ok(procesos);
        }
    }
}
