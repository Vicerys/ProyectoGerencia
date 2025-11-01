using Gerencia.Core.Dtos;
using Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto;
using Gerencia.Presentacion.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class EmpleadoProyectoController : Controller
    {
        //private readonly AppDbContext _db;
        private readonly EmpleadoProyecto_Api _api;
        
        public EmpleadoProyectoController(EmpleadoProyecto_Api api) => _api = api;

        // ----------------- INDEX (Filtro + Orden + Paginación) -----------------
        /*[HttpGet]
       public async Task<IActionResult> Index([Bind(Prefix = "Filtro")] EmpleadoProyectoFilterViewModel filtro, string? mode)
       {
           filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
           filtro.PageSize = 10;
           filtro.MostrarResultados = (mode == "filter");

          if (!filtro.MostrarResultados)
               return View(new EmpleadoProyectoIndexViewModel { Filtro = filtro, Items = Enumerable.Empty<EmpleadoProyectoIndexRowViewModel>() });
           var resultado = await _api.GetFilteredAsync(filtro);

           var vm = new EmpleadoProyectoIndexViewModel
           {
               Filtro = filtro,
               Resultados = resultado,
               Items = resultado
           };

           return View(vm);

       }*/

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(new EmpleadoProyectoIndexViewModel());
        }

        // ----------------- CREATE -----------------
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new EmpleadoProyectoFormViewModel
            {
                EmpleadoProyecto = new EmpleadoProyectoDto
                {
                    FechaAsignacion = DateTime.Today
                },
                Detalles = new List<EmpleadoProyectoDetalleDto>()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadoProyectoFormViewModel vm)
        {
            // Recalcula totales y limpia/numera líneas
            NormalizarYRecalcular(vm);

            if (!ModelState.IsValid)
                return View(vm);

            var response = await _api.CreateAsync(vm.EmpleadoProyecto);
            return RedirectToAction(nameof(Index));
        }
        
        

        // ----------------- EDIT -----------------
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var empleadoProyecto = await _api.GetByIdAsync(id);
            if (empleadoProyecto == null) return NotFound();

            var vm = new EmpleadoProyectoFormViewModel
            {
                EmpleadoProyecto = empleadoProyecto,
                Detalles = empleadoProyecto.Detalles.OrderBy(d => d.Linea).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmpleadoProyectoFormViewModel vm)
        {
            if (id != vm.EmpleadoProyecto.EmpleadoProyectoId) return BadRequest();

            NormalizarYRecalcular(vm);
            if (!ModelState.IsValid)
                return View(vm);

            await _api.UpdateAsync(vm.EmpleadoProyecto);
            return RedirectToAction(nameof(Index));

        }

        // ----------------- DETAILS / DELETE (opcionales) -----------------
        public async Task<IActionResult> Details(int id)
        {
            var empleadoProyecto = await _api.GetByIdAsync(id);   

            if (empleadoProyecto == null) return NotFound();
            return View(empleadoProyecto);
        }
        
        // POST: Pedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pedido = await _api.GetByIdAsync(id);

            _api.DeleteAsync(id).Wait();
            return RedirectToAction(nameof(Index));
            
        }

        // ----------------- Helpers -----------------
        private static void NormalizarYRecalcular(EmpleadoProyectoFormViewModel vm)
        {
            // Asegurar líneas 1..N y redondeos
            var idx = 1;
            foreach (var d in vm.Detalles.OrderBy(d => d.Linea).ToList())
            {
                d.Linea = idx++;
            }
            //Si no repite 2 veces
            //vm.Pedido.Detalles = vm.Detalles;
           // vm.RecalcularTotales();
        }
    }
}

