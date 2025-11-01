using Gerencia.Core.Dtos;
using Gerencia.Presentacion.MVC.Services;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly Empleado_Api _api;
        public EmpleadoController(Empleado_Api api) => _api = api;

        // GET: Empleado
        public async Task<IActionResult> Index()
        {
            var lista = await _api.GetAllAsync();
            return View(lista);
        }

        // GET: Empleado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado= await _api.GetByIdAsync(id.Value);
            return View(empleado);
        }

        // GET: Empleado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleado/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Nombre,PuestoId,FechaNacimiento,Ubicacion,Foto,Usuario,Contrasena")] EmpleadoDto empleado)
        {
            if (ModelState.IsValid)
            {
                await _api.CreateAsync(empleado);
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _api.GetByIdAsync(id.Value);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombre,PuestoId,FechaNacimiento,Ubicacion,Foto,Usuario,Contrasena")] EmpleadoDto empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _api.UpdateAsync(empleado);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _api.GetByIdAsync(id.Value);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _api.GetByIdAsync(id);
            if (empleado != null)
            {
               await _api.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            var empleado = _api.GetByIdAsync(id);

            if (empleado != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
