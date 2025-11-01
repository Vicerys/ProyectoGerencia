using Gerencia.Core.Dtos;
using Gerencia.Core.Repositorios.Entidades;
using Gerencia.Presentacion.MVC.Services;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class ProcesoController : Controller
    {
        private readonly Proceso_Api _api;

        public ProcesoController(Proceso_Api api) => _api = api;

       
        // GET: Proceso
        public async Task<IActionResult> Index()
        {
            var lista = await _api.GetAllAsync();
            return View(lista);
        }

        // GET: Proceso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // GET: Proceso/Create
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProcesoDto proceso)
        {
            if (!ModelState.IsValid)
            {
                return View(proceso);
            }
            await _api.CreateAsync(proceso);
            return RedirectToAction(nameof(Index));
        }

        // GET: Proceso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // POST: Proceso/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProcesoDto proceso)
        {
            if (id != proceso.ProcesoId) return BadRequest();
            if (!ModelState.IsValid) return View(proceso);
            await _api.UpdateAsync(proceso);
            return RedirectToAction(nameof(Index));
        }

        // GET: Proceso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // POST: Proceso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
