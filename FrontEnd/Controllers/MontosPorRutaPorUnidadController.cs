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
    public class MontosPorRutaPorUnidadController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public MontosPorRutaPorUnidadController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: MontosPorRutaPorUnidad
        public async Task<IActionResult> Index()
        {
            var rutasContext = _context.MontosPorRutaPorUnidad.Include(m => m.IdRutaNavigation).Include(m => m.IdUnidadNavigation);
            return View(await rutasContext.ToListAsync());
        }

        // GET: MontosPorRutaPorUnidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new MontosPorRutaPorUnidad().GetType().Name,
                    Objeto = new MontosPorRutaPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var montosPorRutaPorUnidad = await _context.MontosPorRutaPorUnidad
                .Include(m => m.IdRutaNavigation)
                .Include(m => m.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (montosPorRutaPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = montosPorRutaPorUnidad.GetType().Name,
                Objeto = montosPorRutaPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(montosPorRutaPorUnidad);
        }

        // GET: MontosPorRutaPorUnidad/Create
        public IActionResult Create()
        {
            ViewData["IdRuta"] = new SelectList(_context.Rutas, "IdRuta", "IdRuta");
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca");
            return View();
        }

        // POST: MontosPorRutaPorUnidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,IdRuta,IdUnidad,MontoEstimado,MontoRecaudado")] MontosPorRutaPorUnidad montosPorRutaPorUnidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(montosPorRutaPorUnidad);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRuta"] = new SelectList(_context.Rutas, "IdRuta", "IdRuta", montosPorRutaPorUnidad.IdRuta);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", montosPorRutaPorUnidad.IdUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = montosPorRutaPorUnidad.GetType().Name,
                Objeto = montosPorRutaPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(montosPorRutaPorUnidad);
        }

        // GET: MontosPorRutaPorUnidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new MontosPorRutaPorUnidad().GetType().Name,
                    Objeto = new MontosPorRutaPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var montosPorRutaPorUnidad = await _context.MontosPorRutaPorUnidad.FindAsync(id);
            if (montosPorRutaPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            ViewData["IdRuta"] = new SelectList(_context.Rutas, "IdRuta", "IdRuta", montosPorRutaPorUnidad.IdRuta);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", montosPorRutaPorUnidad.IdUnidad);
            return View(montosPorRutaPorUnidad);
        }

        // POST: MontosPorRutaPorUnidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,IdRuta,IdUnidad,MontoEstimado,MontoRecaudado")] MontosPorRutaPorUnidad montosPorRutaPorUnidad)
        {
            if (id != montosPorRutaPorUnidad.Id)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
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
                    _context.Update(montosPorRutaPorUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MontosPorRutaPorUnidadExists(montosPorRutaPorUnidad.Id))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = montosPorRutaPorUnidad.GetType().Name,
                            Objeto = montosPorRutaPorUnidad.ToString(),
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
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRuta"] = new SelectList(_context.Rutas, "IdRuta", "IdRuta", montosPorRutaPorUnidad.IdRuta);
            ViewData["IdUnidad"] = new SelectList(_context.Unidades, "IdUnidad", "NumeroDePlaca", montosPorRutaPorUnidad.IdUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = montosPorRutaPorUnidad.GetType().Name,
                Objeto = montosPorRutaPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(montosPorRutaPorUnidad);
        }

        // GET: MontosPorRutaPorUnidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new MontosPorRutaPorUnidad().GetType().Name,
                    Objeto = new MontosPorRutaPorUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var montosPorRutaPorUnidad = await _context.MontosPorRutaPorUnidad
                .Include(m => m.IdRutaNavigation)
                .Include(m => m.IdUnidadNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (montosPorRutaPorUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = montosPorRutaPorUnidad.GetType().Name,
                    Objeto = montosPorRutaPorUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(montosPorRutaPorUnidad);
        }

        // POST: MontosPorRutaPorUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var montosPorRutaPorUnidad = await _context.MontosPorRutaPorUnidad.FindAsync(id);
            _context.MontosPorRutaPorUnidad.Remove(montosPorRutaPorUnidad);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = montosPorRutaPorUnidad.GetType().Name,
                Objeto = montosPorRutaPorUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool MontosPorRutaPorUnidadExists(int id)
        {
            return _context.MontosPorRutaPorUnidad.Any(e => e.Id == id);
        }
    }
}
