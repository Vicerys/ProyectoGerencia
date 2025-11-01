using Gerencia.Core.Dtos;
using Gerencia.ReglasdeNegocios.Negocios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly UnityofWork _unityofWork;

        public TareaController(UnityofWork unityofWork)
        {
            _unityofWork = unityofWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {

            List<TareaDto> tareas;
            tareas = await _unityofWork.TareaNegocio.ObtenerTodosAsync();
            return Ok(tareas);
        }

        [HttpGet("{tareaId}")]
        public async Task<IActionResult> GetTareaPorId(int tareaId)
        {
            TareaDto tarea;
            tarea = await _unityofWork.TareaNegocio.ObtenerPorIdAsync(tareaId);
            if (tarea == null)
            {
                return NotFound("Empleado no encontrado");
            }
            else
            {
                return Ok(tarea);
            }
        }
    }
}
