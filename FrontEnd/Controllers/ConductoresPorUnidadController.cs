using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd.Datos;
using BackEnd.Entidades;
using BackEnd.Negocio;

namespace FrontEnd.Controllers
{
    public class ConductoresPorUnidadController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public ConductoresPorUnidadController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: ConductoresPorUnidads
        public async Task<IActionResult> Index()
        {
            var rutasContext = _context.ConductoresPorUnidad.Include(c => c.IdConductorNavigation).Include(c => c.IdUnidadNavigation);
            return View(await rutasContext.ToListAsync());
        }

        // GET: ConductoresPorUnidads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new ConductoresPorUnidad().GetType().Name,
                    Objeto = new ConductoresPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductoresPorUnidad = await _context.ConductoresPorUnidad
                .Include(c => c.IdConductorNavigation)
                .Include(c => c.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conductoresPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = conductoresPorUnidad.GetType().Name,
                Objeto = conductoresPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(conductoresPorUnidad);
        }

        // GET: ConductoresPorUnidads/Create
        public IActionResult Create()
        {
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "IdConductor", "NombreCompleto");
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca");
            return View();
        }

        // POST: ConductoresPorUnidads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUnidad,IdConductor")] ConductoresPorUnidad conductoresPorUnidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conductoresPorUnidad);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "IdConductor", "NombreCompleto", conductoresPorUnidad.IdConductor);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", conductoresPorUnidad.IdUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = conductoresPorUnidad.GetType().Name,
                Objeto = conductoresPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(conductoresPorUnidad);
        }

        // GET: ConductoresPorUnidads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new ConductoresPorUnidad().GetType().Name,
                    Objeto = new ConductoresPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductoresPorUnidad = await _context.ConductoresPorUnidad.FindAsync(id);
            if (conductoresPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "IdConductor", "NombreCompleto", conductoresPorUnidad.IdConductor);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", conductoresPorUnidad.IdUnidad);
            return View(conductoresPorUnidad);
        }

        // POST: ConductoresPorUnidads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUnidad,IdConductor")] ConductoresPorUnidad conductoresPorUnidad)
        {
            if (id != conductoresPorUnidad.Id)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conductoresPorUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConductoresPorUnidadExists(conductoresPorUnidad.Id))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = conductoresPorUnidad.GetType().Name,
                            Objeto = conductoresPorUnidad.ToString(),
                            Usuario = HttpContext.User.Identity.Name,
                            Completada = false,
                            FechaHora = DateTime.Now
                        });

                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConductor"] = new SelectList(_context.Conductores, "IdConductor", "NombreCompleto", conductoresPorUnidad.IdConductor);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", conductoresPorUnidad.IdUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = conductoresPorUnidad.GetType().Name,
                Objeto = conductoresPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(conductoresPorUnidad);
        }

        // GET: ConductoresPorUnidads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new ConductoresPorUnidad().GetType().Name,
                    Objeto = new ConductoresPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductoresPorUnidad = await _context.ConductoresPorUnidad
                .Include(c => c.IdConductorNavigation)
                .Include(c => c.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conductoresPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = conductoresPorUnidad.GetType().Name,
                    Objeto = conductoresPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(conductoresPorUnidad);
        }

        // POST: ConductoresPorUnidads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conductoresPorUnidad = await _context.ConductoresPorUnidad.FindAsync(id);
            _context.ConductoresPorUnidad.Remove(conductoresPorUnidad);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = conductoresPorUnidad.GetType().Name,
                Objeto = conductoresPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool ConductoresPorUnidadExists(int id)
        {
            return _context.ConductoresPorUnidad.Any(e => e.Id == id);
        }
    }
}
