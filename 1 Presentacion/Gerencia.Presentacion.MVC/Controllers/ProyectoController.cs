using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using ProyectoEntity = ProyectoFinal_MVC.Models.Proyecto;
using Gerencia.Presentacion.MVC.Models.ViewModels.Proyecto;
using Gerencia.Core.Repositorios.Entidades;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Infrastructure;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly AppDbContext _db;
        public ProyectoController(AppDbContext db) { _db = db; }

        // ---------- INDEX: FILTRO + PAGINACIÓN ----------
        private async Task<ProyectoFilterViewModel> HydrateFilterAsync(ProyectoFilterViewModel f)
        {
            return f;
        }

        [HttpGet]
        public async Task<IActionResult> Index(
            [Bind(Prefix = "Filtro")] ProyectoFilterViewModel filtro,
            string? mode)
        {
            filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
            filtro.PageSize = 10;
            await HydrateFilterAsync(filtro);

            IQueryable<ProyectoEntidad> q = _db.Proyectos
                .AsNoTracking();

            bool aplicarFiltro = mode == "filter" ||
                (!filtro.MostrarTodos && (
                    filtro.ProyectoID.HasValue ||
                    !string.IsNullOrWhiteSpace(filtro.NombreLike) ||
                    filtro.FechaTarget != DateTarget.None));

            if (aplicarFiltro)
            {
                if (filtro.ProyectoID.HasValue) q = q.Where(p => p.ProyectoId == filtro.ProyectoID.Value);
                if (!string.IsNullOrWhiteSpace(filtro.NombreLike))
                    q = q.Where(p => EF.Functions.Like(p.Nombre, $"%{filtro.NombreLike}%"));
                
                if (filtro.FechaTarget != DateTarget.None && (filtro.FechaDesde.HasValue || filtro.FechaHasta.HasValue))
                {
                    DateTime? desde = filtro.FechaDesde?.Date;
                    DateTime? hasta = filtro.FechaHasta?.Date.AddDays(1).AddTicks(-1);
                    if (filtro.FechaTarget == DateTarget.FechaLimite)
                    {
                        if (desde.HasValue) q = q.Where(p => p.FechaLimite >= desde.Value);
                        if (hasta.HasValue) q = q.Where(p => p.FechaLimite <= hasta.Value);
                    }

                }
            }

            q = q.OrderBy(p => p.ProyectoId);
            var paged = await PaginatedList<ProyectoEntidad>.CreateAsync(q, filtro.Page, filtro.PageSize);
            var vm = new ProyectoIndexViewModel { Filtro = filtro, Resultados = paged };
            return View(vm);
        }

        // ---------- CREATE/EDIT con ViewModel ----------
        private async Task<ProyectoFormViewModel> BuildFormVMAsync(ProyectoEntidad? p = null)
        {
            var vm = new ProyectoFormViewModel
            {
                /*Proyecto = p ?? new ProyectoEntidad
                {
                    FechaLimite = DateTime.Today
                }*/
            };

            return vm;
        }

        // GET: Proyecto/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = await BuildFormVMAsync();
            return View(vm);
        }

        // POST: Proyecto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProyectoFormViewModel vm)
        {
            //_db.Proyectos.Add(vm.Proyecto);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Producto/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var p = await _db.Proyectos.FindAsync(id);
            if (p == null) return NotFound();

            var vm = await BuildFormVMAsync(p);
            return View(vm);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProyectoFormViewModel vm)
        {
            if (id != vm.Proyecto.ProyectoId) return BadRequest();

            if (!ModelState.IsValid)
            {
                //var reload = await BuildFormVMAsync(vm.Proyecto);
 
                return View();
            }

            _db.Entry(vm.Proyecto).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ---------- DETAILS/DELETE (solo lectura con entidad) ----------
        public async Task<IActionResult> Details(int id)
        {
            var p = await _db.Proyectos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProyectoId == id);
            if (p == null) return NotFound();
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _db.Proyectos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProyectoId == id);
            if (p == null) return NotFound();
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _db.Proyectos.FindAsync(id);
            if (p == null) return NotFound();

            _db.Proyectos.Remove(p);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Acción JSON
        [HttpGet]
        public async Task<IActionResult> Search(string? term, int take = 20)
        {
            var q = _db.Proyectos.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(term)) q = q.Where(p => EF.Functions.Like(p.Nombre, $"%{term}%"));
            var items = await q.OrderBy(p => p.ProyectoId).Take(take)
                .Select(p => new { p.ProyectoId, p.Nombre, p.FechaLimite })
                .ToListAsync();
            return Json(items);
        }

        //Filtro Proyecto
        // GET: /Proyecto/LookupMeta
        // Devuelve listas para popular el modal
        /*[HttpGet]
        public async Task<IActionResult> LookupMeta()
        {
            var lineas = await _db.ProductoLineas.AsNoTracking()
                .OrderBy(x => x.Nombre)
                .Select(x => new { id = x.ProductoLineaId, nombre = x.Nombre })
                .ToListAsync();

            var statuses = await _db.ProductoStatuses.AsNoTracking()
                .OrderBy(s => s.ProductoStatusCodigo)
                .Select(s => new { codigo = s.ProductoStatusCodigo, nombre = s.Nombre })
                .ToListAsync();

            return Json(new { lineas, statuses });
        }

        // GET: /Producto/Lookup
        // Filtro para el modal: id, lineaId, term (nombre contiene), status (lista), take (límite)
        [HttpGet]
        public async Task<IActionResult> Lookup(int? id, int? lineaId, string? term,
                                                [FromQuery] List<char> status, int take = 50)
        {
            var q = _db.Productos.AsNoTracking()
                .Include(p => p.ProductoLinea)
                .Include(p => p.ProductoStatus)
                .AsQueryable();

            if (id.HasValue) q = q.Where(p => p.ProductoID == id.Value);
            if (lineaId.HasValue) q = q.Where(p => p.ProductoLineaId == lineaId.Value);
            if (!string.IsNullOrWhiteSpace(term))
                q = q.Where(p => EF.Functions.Like(p.Nombre, $"%{term}%"));
            if (status != null && status.Count > 0)
            {
                var set = status.ToHashSet();
                q = q.Where(p => set.Contains(p.ProductoStatusCodigo));
            }

            var items = await q.OrderBy(p => p.ProductoID).Take(take)
                .Select(p => new
                {
                    p.ProductoID,
                    p.Nombre,
                    Linea = p.ProductoLinea!.Nombre,
                    p.PrecioUnitario,
                    StatusCodigo = p.ProductoStatusCodigo,
                    StatusNombre = p.ProductoStatus!.Nombre
                })
                .ToListAsync();

            return Json(items);
        }*/
    }
}

