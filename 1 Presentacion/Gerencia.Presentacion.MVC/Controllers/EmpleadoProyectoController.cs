using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerencia.Presentacion.MVC.Models;
using Gerencia.Presentacion.MVC.Models.ViewModels.EmpleadoProyecto;
using Gerencia.ReglasdeNegocios.Repositorios.SQL.Contexts;

namespace Gerencia.Presentacion.MVC.Controllers
{
    public class EmpleadoProyectoController : Controller
    {
        private readonly AppDbContext _db;
        public EmpleadoProyectoController(AppDbContext db) => _db = db;

        // -------------------------------
        // Utilitario: catálogos del modal de productos
        // -------------------------------
        /*private async Task LoadCatalogsAsync(EmpleadoProyectoFormViewModel vm)
        {
            vm.ProductoLineasOptions = await _db.ProductoLineas
                .AsNoTracking()
                .OrderBy(x => x.Nombre)
                .Select(x => new SelectListItem { Value = x.ProductoLineaId.ToString(), Text = x.Nombre })
                .ToListAsync();

            vm.ProductoStatuses = await _db.ProductoStatuses
                .AsNoTracking()
                .OrderBy(x => x.ProductoStatusCodigo)
                .ToListAsync();
        }*/

        // -------------------------------
        // Index (lista simple; si usas un VM de filtro, puedes cambiar la firma)
        // -------------------------------
        public async Task<IActionResult> Index([FromQuery] EmpleadoProyectoIndexViewModel vm, [FromQuery] bool showAll = false)
        {
            // Si es la primera vez (sin query) y no pidió "Mostrar todos", no traigas nada
            if (!showAll && Request.Query.Count == 0)
            {
                vm.Resultados = new List<EmpleadoProyecto>();
                return View(vm);
            }

            var q = _db.EmpleadoProyectos
                .AsNoTracking()
                .Include(p => p.Empleado)
                .AsQueryable();

            if (vm.EmpleadoProyectoID.HasValue)
                q = q.Where(p => p.EmpleadoProyectoId == vm.EmpleadoProyectoID.Value);

            if (vm.EmpleadoID.HasValue)
                q = q.Where(p => p.EmpleadoId == vm.EmpleadoID.Value);

            if (!string.IsNullOrWhiteSpace(vm.EmpleadoNombre))
                q = q.Where(p => p.Empleado != null && p.Empleado.Nombre.Contains(vm.EmpleadoNombre));

            if (vm.FechaDesde.HasValue)
                q = q.Where(p => p.FechaAsignacion >= vm.FechaDesde.Value.Date);

            if (vm.FechaHasta.HasValue)
            {
                var hasta = vm.FechaHasta.Value.Date.AddDays(1).AddTicks(-1);
                q = q.Where(p => p.FechaAsignacion <= hasta);
            }

            /*vm.Resultados = await q
                .OrderByDescending(p => p.EmpleadoProyectoId)
                .Take(200)
                .ToListAsync();*/

            return View(vm);
        }

        // -------------------------------
        // Create (GET)
        // -------------------------------
        public async Task<IActionResult> Create()
        {
            var vm = new EmpleadoProyectoFormViewModel
            {
                /*EmpleadoProyecto = new Models.EmpleadoProyecto
                {
                    FechaAsignacion = DateTime.Today
                }*/
            };
            //await LoadCatalogsAsync(vm);
            return View(vm);
        }

        // -------------------------------
        // Create (POST con comandos)
        //  - BuscarCliente / selectClienteId
        //  - BuscarProducto / selectProductoId
        //  - AddLine / removeIndex
        //  - Save
        // -------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            EmpleadoProyectoFormViewModel vm,
            string? command,
            int? removeIndex,
            int? nuevoProyectoId,
            decimal? nuevaCantidad,
            int? selectEmpleadoId,
            int? selectProyectoId)
        {
            vm.Detalles ??= new List<EmpleadoProyectoDetalleViewModel>();

            // --- Modal: filtrar empleados
            if (command == "BuscarEmpleado")
            {
                var cq = _db.Empleados.AsNoTracking().AsQueryable();

                if (vm.FiltroEmpleadoId.HasValue)
                    cq = cq.Where(c => c.EmpleadoId == vm.FiltroEmpleadoId.Value);

                if (!string.IsNullOrWhiteSpace(vm.FiltroEmpleadoNombre))
                    cq = cq.Where(c => c.Nombre.Contains(vm.FiltroEmpleadoNombre));

                vm.EmpleadoResultados = await cq
                    .OrderBy(c => c.EmpleadoId)
                    .Take(100)
                    .Select(c => new EmpleadoLookupRow
                    {
                        EmpleadoId = c.EmpleadoId,
                        Nombre = c.Nombre
                    })
                    .ToListAsync();

                vm.ShowEmpleadoModal = true;
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            // --- Modal: seleccionar cliente
            if (selectEmpleadoId.HasValue)
            {
                var cli = await _db.Empleados.AsNoTracking().FirstOrDefaultAsync(x => x.EmpleadoId == selectEmpleadoId.Value);
                if (cli != null)
                {
                    vm.EmpleadoProyecto.EmpleadoId = cli.EmpleadoId;
                    vm.EmpleadoNombre = cli.Nombre;
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            // --- Modal: filtrar productos
            if (command == "BuscarProyecto")
            {
                //await LoadCatalogsAsync(vm);

                var pq = _db.Proyectos
                    .AsNoTracking()
                    .AsQueryable();

                if (vm.FiltroProyectoId.HasValue)
                    pq = pq.Where(p => p.ProyectoId == vm.FiltroProyectoId.Value);

                // Proyección con null-safety para evitar CS8602
                vm.ProyectoResultados = await pq
                    .OrderBy(p => p.ProyectoId)
                    .Take(100)
                    .Select(p => new ProductoLookupRow
                    {
                        ProyectoID = p.ProyectoId,
                        Nombre = p.Nombre,
                        FechaLimite = p.FechaLimite

                    })
                    .ToListAsync();

                vm.ShowProyectoModal = true;
                ModelState.Clear();
                return View(vm);
            }

            // --- Modal: seleccionar producto -> agrega línea
            if (selectProyectoId.HasValue)
            {
                var proy = await _db.Proyectos.AsNoTracking().FirstOrDefaultAsync(x => x.ProyectoId == selectProyectoId.Value);
                if (proy != null)
                {
                    vm.Detalles.Add(new EmpleadoProyectoDetalleViewModel
                    {
                        Linea = vm.Detalles.Count + 1,
                        ProyectoId = proy.ProyectoId,
                        
                    });
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            // --- Agregar línea por ID directo
            if (command == "AddLine" && nuevoProyectoId.HasValue)
            {
                var prod = await _db.Proyectos.AsNoTracking().FirstOrDefaultAsync(x => x.ProyectoId == nuevoProyectoId.Value);
                if (prod != null)
                {
                    vm.Detalles.Add(new EmpleadoProyectoDetalleViewModel
                    {
                        Linea = vm.Detalles.Count + 1,
                        ProyectoId = prod.ProyectoId,
                        
                    });
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            // --- Quitar línea
            if (removeIndex.HasValue)
            {
                var idx = removeIndex.Value;
                if (idx >= 0 && idx < vm.Detalles.Count) vm.Detalles.RemoveAt(idx);
                for (int i = 0; i < vm.Detalles.Count; i++) vm.Detalles[i].Linea = i + 1;

                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            // --- Guardar
            if (command == "Save")
            {
                if (!ModelState.IsValid)
                {
                    //await LoadCatalogsAsync(vm);
                    return View(vm);
                }

                await using var tx = await _db.Database.BeginTransactionAsync();
                try
                {
                    var asign = vm.EmpleadoProyecto;

                    _db.EmpleadoProyectos.Add(asign);
                    await _db.SaveChangesAsync();

                    int linea = 1;
                    foreach (var d in vm.Detalles)
                    {
                       /* _db.EmpleadoProyectoDetalles.Add(new Models.EmpleadoProyectoDetalle
                        {
                            EmpleadoProyectoId = asign.EmpleadoProyectoId,
                            Linea = linea++,
                            ProyectoID = d.ProyectoId
                        });*/
                    }

                    await _db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }

            //await LoadCatalogsAsync(vm);
            return View(vm);
        }

        // -------------------------------
        // Edit (GET)
        // -------------------------------
        public async Task<IActionResult> Edit(int id)
        {
            var asign = await _db.EmpleadoProyectos
                .Include(p => p.Empleado)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.EmpleadoProyectoId == id);

            if (asign == null) return NotFound();

            var vm = new EmpleadoProyectoFormViewModel
            {
                EmpleadoProyecto = asign,
                EmpleadoNombre = asign.Empleado?.Nombre ?? "",
                Detalles = asign.Detalles
                    .OrderBy(d => d.Linea)
                    .Select(d => new EmpleadoProyectoDetalleViewModel
                    {
                        Linea = d.Linea,
                        EmpleadoProyectoId = d.EmpleadoProyectoId
                        
                    })
                    .ToList()
            };

            //await LoadCatalogsAsync(vm);
            return View(vm);
        }

        // -------------------------------
        // Edit (POST con mismos comandos)
        // -------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            EmpleadoProyectoFormViewModel vm,
            string? command,
            int? removeIndex,
            int? nuevoProyectoId,
            decimal? nuevaCantidad,
            int? selectEmpleadoId,
            int? selectProyectoId)
        {
            vm.Detalles ??= new List<EmpleadoProyectoDetalleViewModel>();

            // Reutilizamos exactamente las mismas ramas que en Create
            if (command == "BuscarEmpleado")
            {
                var cq = _db.Empleados.AsNoTracking().AsQueryable();

                if (vm.FiltroEmpleadoId.HasValue)
                    cq = cq.Where(c => c.EmpleadoId == vm.FiltroEmpleadoId.Value);

                if (!string.IsNullOrWhiteSpace(vm.FiltroEmpleadoNombre))
                    cq = cq.Where(c => c.Nombre.Contains(vm.FiltroEmpleadoNombre));

                vm.EmpleadoResultados = await cq
                    .OrderBy(c => c.EmpleadoId)
                    .Take(100)
                    .Select(c => new EmpleadoLookupRow
                    {
                        EmpleadoId = c.EmpleadoId,
                        Nombre = c.Nombre,
                        
                    })
                    .ToListAsync();

                vm.ShowEmpleadoModal = true;
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            if (selectEmpleadoId.HasValue)
            {
                var cli = await _db.Empleados.AsNoTracking().FirstOrDefaultAsync(x => x.EmpleadoId == selectEmpleadoId.Value);
                if (cli != null)
                {
                    vm.EmpleadoProyecto.EmpleadoId = cli.EmpleadoId;
                    vm.EmpleadoNombre = cli.Nombre;
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            if (command == "BuscarProyecto")
            {
                //await LoadCatalogsAsync(vm);

                var pq = _db.Proyectos
                    .AsNoTracking()
                    .AsQueryable();

                if (vm.FiltroProyectoId.HasValue)
                    pq = pq.Where(p => p.ProyectoId == vm.FiltroProyectoId.Value);

                vm.ProyectoResultados = await pq
                    .OrderBy(p => p.ProyectoId)
                    .Take(100)
                    .Select(p => new ProductoLookupRow
                    {
                        ProyectoID = p.ProyectoId,
                        Nombre = p.Nombre,
                        
                    })
                    .ToListAsync();

                vm.ShowProyectoModal = true;
                ModelState.Clear();
                return View(vm);
            }

            if (selectProyectoId.HasValue)
            {
                var prod = await _db.Proyectos.AsNoTracking().FirstOrDefaultAsync(x => x.ProyectoId == selectProyectoId.Value);
                if (prod != null)
                {
                    vm.Detalles.Add(new EmpleadoProyectoDetalleViewModel
                    {
                        Linea = vm.Detalles.Count + 1,
                        ProyectoId = prod.ProyectoId

                    });
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            if (command == "AddLine" && nuevoProyectoId.HasValue)
            {
                var prod = await _db.Proyectos.AsNoTracking().FirstOrDefaultAsync(x => x.ProyectoId == nuevoProyectoId.Value);
                if (prod != null)
                {
                    vm.Detalles.Add(new EmpleadoProyectoDetalleViewModel
                    {
                        Linea = vm.Detalles.Count + 1,
                        ProyectoId = prod.ProyectoId,
                        
                    });
                }
                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            if (removeIndex.HasValue)
            {
                var idx = removeIndex.Value;
                if (idx >= 0 && idx < vm.Detalles.Count) vm.Detalles.RemoveAt(idx);
                for (int i = 0; i < vm.Detalles.Count; i++) vm.Detalles[i].Linea = i + 1;

                //await LoadCatalogsAsync(vm);
                ModelState.Clear();
                return View(vm);
            }

            if (command == "Save")
            {
                if (!ModelState.IsValid)
                {
                   // await LoadCatalogsAsync(vm);
                    return View(vm);
                }

                await using var tx = await _db.Database.BeginTransactionAsync();
                try
                {
                    var asign = await _db.EmpleadoProyectos
                        .Include(p => p.Detalles)
                        .FirstOrDefaultAsync(p => p.EmpleadoProyectoId == id);

                    if (asign == null) return NotFound();

                    // Cabecera
                    asign.EmpleadoId = vm.EmpleadoProyecto.EmpleadoId;
                    asign.FechaAsignacion = vm.EmpleadoProyecto.FechaAsignacion;
                    

                    // Reemplazo simple de detalles
                    //_db.EmpleadoProyectoDetalles.RemoveRange(asign.Detalles);
                    int linea = 1;
                    foreach (var d in vm.Detalles)
                    {
                        /*asign.Detalles.Add(new Models.EmpleadoProyectoDetalle
                        {
                            EmpleadoProyectoId = asign.EmpleadoProyectoId,
                            Linea = linea++,
                            ProyectoID = d.ProyectoId,
                            
                        });*/
                    }

                    await _db.SaveChangesAsync();
                    await tx.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    await tx.RollbackAsync();
                    throw;
                }
            }

            //await LoadCatalogsAsync(vm);
            return View(vm);
        }

        // GET: Pedido/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pedido = await _db.EmpleadoProyectos
                .AsNoTracking()
                .Include(p => p.Empleado)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.EmpleadoProyectoId == id);

            if (pedido == null)
                return NotFound();

            return View(pedido); // La vista Details recibe un Modelo: SistemaEmpresarial.Models.Pedido.Pedido
        }

        // GET: Pedido/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _db.EmpleadoProyectos
                .AsNoTracking()
                .Include(p => p.Empleado)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.EmpleadoProyectoId == id);

            if (pedido == null)
                return NotFound();

            return View(pedido); // La vista Delete muestra datos y pide confirmación
        }

        // POST: Pedido/Delete/5 (confirmación)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await using var tx = await _db.Database.BeginTransactionAsync();
            try
            {
                var pedido = await _db.EmpleadoProyectos
                    .Include(p => p.Detalles)
                    .FirstOrDefaultAsync(p => p.EmpleadoProyectoId == id);

                if (pedido == null)
                    return NotFound();

                // Si deseas restringir borrado a solo pedidos Pendientes, descomenta:
                // if (pedido.PedidoStatusCodigo != 'P')
                // {
                //     ModelState.AddModelError("", "Solo pueden borrarse pedidos en estado Pendiente.");
                //     return View("Delete", pedido);
                // }

                //_db.EmpleadoProyectoDetalles.RemoveRange(pedido.Detalles);
                _db.EmpleadoProyectos.Remove(pedido);
                await _db.SaveChangesAsync();
                await tx.CommitAsync();

                // Opcional: feedback al usuario
                TempData["Ok"] = $"Pedido {id} eliminado.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                await tx.RollbackAsync();
                throw;
            }
        }
    }
}
