using Gerencia.Core.Dtos;
using Gerencia.Core.Repositorios.Entidades;
using Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto;
using Gerencia.Presentacion.MVC.Services;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly Proyecto_Api _api;

        public ProyectoController(Proyecto_Api api) => _api = api;


        // GET: Proyecto
        public async Task<IActionResult> Index()
        {
            var lista = await _api.GetAllAsync();
            return View(new ProyectoIndexViewModel());
        }

        // GET: Proyecto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // GET: Proyecto/Create
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProyectoDto proyecto)
        {
            if (!ModelState.IsValid)
            {
                return View(proyecto);
            }
            await _api.CreateAsync(proyecto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Proyecto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // POST: Proyecto/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProyectoDto proyecto)
        {
            if (id != proyecto.ProyectoId) return BadRequest();
            if (!ModelState.IsValid) return View(proyecto);
            await _api.UpdateAsync(proyecto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Proyecto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();
            var c = await _api.GetByIdAsync(id.Value);
            return c is null ? NotFound() : View(c);
        }

        // POST: Proyecto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _api.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
