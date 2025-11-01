using Gerencia.Core.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Gerencia.ReglasdeNegocios.Negocios;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPuestoController : ControllerBase
    {
        private readonly TipoPuestoNegocio _tipoPuestoNegocio;

        public TipoPuestoController(TipoPuestoNegocio tipoPuestoNegocio)
        {
            _tipoPuestoNegocio = tipoPuestoNegocio;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposPuesto()
        {
            List<TipoPuestoDto> tiposPuesto;
            tiposPuesto= await _tipoPuestoNegocio.ObtenerTodosAsync();

            return Ok(tiposPuesto);
        }
    }
}
