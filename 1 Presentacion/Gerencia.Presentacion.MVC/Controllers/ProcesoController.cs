using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProcesoEntity = Gerencia.Presentacion.MVC.Models.Proceso;
using Gerencia.Presentacion.MVC.Models.ViewModels.Proceso;
using Gerencia.Presentacion.MVC.Models.ViewModels;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class ProcesoController : Controller
    {
            private readonly AppDbContext _db;
            public ProcesoController(AppDbContext db) { _db = db; }

            // ---------- INDEX: FILTRO + PAGINACIÓN ----------
            private async Task<ProcesoFilterViewModel> HydrateFilterAsync(ProcesoFilterViewModel f)
            {
                f.Importancia = await _db.Importancias.AsNoTracking()
                    .OrderBy(x => x.Nombre)
                    .Select(x => new SelectListItem(x.Nombre, x.ImportanciaId.ToString(),
                                                    f.ImportanciaId == x.ImportanciaId))
                    .ToListAsync();

                f.Statuses = await _db.Estados.AsNoTracking()
                    .OrderBy(x => x.EstadoId)
                    .Select(x => new SelectListItem(x.Nombre, x.EstadoId.ToString(),
                                                    f.ProcesoStatus.Contains(x.EstadoId.ToString())))
                    .ToListAsync();

               
                return f;
            }

            [HttpGet]
            public async Task<IActionResult> Index(
                [Bind(Prefix = "Filtro")] ProcesoFilterViewModel filtro,
                string? mode)
            {
                filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
                filtro.PageSize = 10;
                await HydrateFilterAsync(filtro);

            IQueryable<ProcesoEntity> q ;/*=_db.Procesos
                    .AsNoTracking()
                    .Include(p => p.Importancia)
                    .Include(p => p.Estado);*/

             /*
                bool aplicarFiltro = mode == "filter" ||
                    (!filtro.MostrarTodos && (
                        filtro.ProcesoID.HasValue ||
                        !string.IsNullOrWhiteSpace(filtro.NombreLike) ||
                        filtro.ImportanciaId.HasValue ||
                        filtro.Finalizado.HasValue ||
                        filtro.Proyecto1== true || filtro.Proyecto2 == true || filtro.Proyecto3 == true ||
                        filtro.FechaTarget != DateTarget.None ||
                        (filtro.ProcesoStatus?.Any() ?? false)));

                if (aplicarFiltro)
                {
                    if (filtro.ProcesoID.HasValue) q = q.Where(p => p.ProcesoId == filtro.ProcesoID.Value);
                    if (!string.IsNullOrWhiteSpace(filtro.NombreLike))
                        q = q.Where(p => EF.Functions.Like(p.Nombre, $"%{filtro.NombreLike}%"));
                    if (filtro.ImportanciaId.HasValue) q = q.Where(p => p.ImportanciaId == filtro.ImportanciaId.Value);
                    if (filtro.Finalizado.HasValue) q = q.Where(p => p.Finalizado == filtro.Finalizado.Value);
                    if (filtro.Proyecto1 == true) q = q.Where(p => p.ProyectoId==7);
                    if (filtro.Proyecto2 == true) q = q.Where(p => p.ProyectoId==2);
                    if (filtro.Proyecto3 == true) q = q.Where(p => p.ProyectoId==3);
                    if (filtro.FechaTarget != DateTarget.None && (filtro.FechaDesde.HasValue || filtro.FechaHasta.HasValue))
                    {
                        DateTime? desde = filtro.FechaDesde?.Date;
                        DateTime? hasta = filtro.FechaHasta?.Date.AddDays(1).AddTicks(-1);
                        if (filtro.FechaTarget == DateTarget.FechaEstimada)
                        {
                            if (desde.HasValue) q = q.Where(p => p.FechaTerminoEstimada >= desde.Value);
                            if (hasta.HasValue) q = q.Where(p => p.FechaTerminoEstimada <= hasta.Value);
                        }
                        else if (filtro.FechaTarget == DateTarget.FechaReal)
                        {
                            if (desde.HasValue) q = q.Where(p => p.FechaTerminoReal >= desde.Value);
                            if (hasta.HasValue) q = q.Where(p => p.FechaTerminoReal <= hasta.Value);
                        }
                    }
                    if ((filtro.ProcesoStatus != null) && (filtro.ProcesoStatus.Any()))
                    {
                        var setStatus = filtro.ProcesoStatus.Select(s => int.Parse(s)).ToHashSet();
                        q = q.Where(p => setStatus.Contains(p.EstadoId));
                    }
                }

                q = q.OrderBy(p => p.ProcesoId);
                var paged = await PaginatedList<ProcesoEntity>.CreateAsync(q, filtro.Page, filtro.PageSize);
                var vm = new ProcesoIndexViewModel { Filtro = filtro, Resultados = paged };*/
                return View();
            }

            // ---------- CREATE/EDIT con ViewModel ----------
            private async Task<ProcesoFormViewModel> BuildFormVMAsync(ProcesoEntity? p = null)
            {
                var vm = new ProcesoFormViewModel
                {
                    Proceso = p ?? new ProcesoEntity
                    {
                        FechaTerminoEstimada = DateTime.Today,
                        EstadoId = '1'
                    },
                    Importancias = await _db.Importancias.AsNoTracking()
                        .OrderBy(x => x.Nombre)
                        .Select(x => new SelectListItem(x.Nombre, x.ImportanciaId.ToString(),
                                                        p != null && p.ImportanciaId == x.ImportanciaId))
                        .ToListAsync(),
                    
                    Estados = await _db.Estados.AsNoTracking()
                        .OrderBy(x => x.EstadoId)
                        .Select(x => new SelectListItem(x.Nombre, x.EstadoId.ToString(),
                                                        p != null && p.EstadoId == x.EstadoId))
                        .ToListAsync(),
                    
                };
                return vm;
            }

            // GET: Producto/Create
            [HttpGet]
            public async Task<IActionResult> Create()
            {
                var vm = await BuildFormVMAsync();
                return View(vm);
            }

            // POST: Producto/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(ProcesoFormViewModel vm)
            {
                

                if (!ModelState.IsValid)
                {
                    var reload = await BuildFormVMAsync(vm.Proceso);
                    return View(reload);
                }

                //_db.Procesos.Add(vm.Proceso);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Producto/Edit/5
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var p = await _db.Procesos.FindAsync(id);
                if (p == null) return NotFound();

                //var vm = await BuildFormVMAsync(p);
                     
            return View();
            }

            // POST: Producto/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, ProcesoFormViewModel vm)
            {
                if (id != vm.Proceso.ProcesoId) return BadRequest();

                if (!ModelState.IsValid)
                {
                    var reload = await BuildFormVMAsync(vm.Proceso);
                    
                    return View(reload);
                }

                _db.Entry(vm.Proceso).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ---------- DETAILS/DELETE (solo lectura con entidad) ----------
            public async Task<IActionResult> Details(int id)
            {
                var p = await _db.Procesos
                    .Include(x => x.Importancia)
                    .Include(x => x.Estado)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProcesoId == id);
                if (p == null) return NotFound();
                return View(p);
            }

            public async Task<IActionResult> Delete(int id)
            {
                var p = await _db.Procesos
                    .Include(x => x.Importancia)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProcesoId == id);
                if (p == null) return NotFound();
                return View(p);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var p = await _db.Procesos.FindAsync(id);
                if (p == null) return NotFound();

                _db.Procesos.Remove(p);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }

    
    
    /*    private readonly AppDbContext _context;

            public ProcesoController(AppDbContext context)
            {
                _context = context;
            }

            private async Task<ProcesoFormViewModel> BuildFormVMAsync(Proceso? p = null)
            {
                var vm = new ProcesoFormViewModel
                {
                    Proceso = p ?? new Proceso { ImportanciaId=1,FechaTerminoEstimada=DateTime.Today,EstadoId=1 },

                    Estados = await _context.Estados.AsNoTracking()
                        .OrderBy(x => x.EstadoId)
                        .Select(x => new SelectListItem(x.Nombre,
                             x.EstadoId.ToString(), p != null && p.EstadoId == x.EstadoId))
                        .ToListAsync(),
                    Importancias = await _context.Importancias.AsNoTracking()
                        .OrderBy(x => x.ImportanciaId)
                        .Select(x => new SelectListItem(x.Nombre,
                             x.ImportanciaId.ToString(), p != null && p.ImportanciaId == x.ImportanciaId))
                        .ToListAsync(),
                };
                return vm;
            }

            // GET: Proceso
            public async Task<IActionResult> Index()
            {
                var lista = await _context.Procesos
                    .Include(p => p.Importancia)
                    .Include(p => p.Estado)
                    .OrderBy(p => p.ProcesoId)
                    .ToListAsync();
                return View(lista);
            }

            // GET: Proceso/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                    var p = await _context.Procesos
                    .Include(p => p.Estado)
                    .Include(p => p.Importancia)
                    .FirstOrDefaultAsync(m => m.ProcesoId == id);

                if(p == null) return NotFound();
                return View(p);
            }

            // GET: Proceso/Create
            public async Task<IActionResult> Create()
            {
                var vm = await BuildFormVMAsync();
                return View(vm);
            }

            // POST: Proceso/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(ProcesoFormViewModel vm)
            {
                if (!ModelState.IsValid)
                {
                    //Recargar listas si hay error
                    var reload = await BuildFormVMAsync(vm.Proceso);
                    return View(reload);
                }

                _context.Procesos.Add(vm.Proceso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // GET: Proceso/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var p = await _context.Procesos.FindAsync(id);
                if (p == null)
                {
                    return NotFound();
                }
                var vm = await BuildFormVMAsync(p);
                return View(vm);
            }   

            // POST: Proceso/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598. 
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, ProcesoFormViewModel vm)
            {
                if (id != vm.Proceso.ProcesoId)
                {
                    return BadRequest();
                }
                if (!ModelState.IsValid)
                {
                    //Recargar listas si hay error
                    var reload = await BuildFormVMAsync(vm.Proceso);
                    return View(reload);
                }

                _context.Entry(vm.Proceso).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }

            // GET: Proceso/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var p = await _context.Procesos
                    .Include(x => x.Importancia)
                    .Include(x => x.Estado)
                    .FirstOrDefaultAsync(x => x.ProcesoId == id);

                if (p == null)
                {
                    return NotFound();
                }
                return View(p);
            }

            // POST: Proceso/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var p = await _context.Procesos.FindAsync(id);

                if (p == null)
                {
                    return NotFound();
                }

                _context.Procesos.Remove(p);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

        }
    }

    */