using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportanciaController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public ImportanciaController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetImportancia()
        {
            List<ImportanciaDto> importancias;
            importancias = await _unityofWork.ImportanciaNegocio.ObtenerTodosAsync();

            return Ok(importancias);
        }
    }
}
