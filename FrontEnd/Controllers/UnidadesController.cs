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
    public class UnidadesController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public UnidadesController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: Unidades
        public async Task<IActionResult> Index()
        {
            var rutasContext = _context.Unidades.Include(u => u.IdEstadoDeUnidadNavigation);
            return View(await rutasContext.ToListAsync());
        }

        // GET: Unidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new Unidades().GetType().Name,
                    Objeto = new Unidades  ().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var unidades = await _context.Unidades
                .Include(u => u.IdEstadoDeUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidades == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = unidades.GetType().Name,
                Objeto = unidades.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(unidades);
        }

        // GET: Unidades/Create
        public IActionResult Create()
        {
            ViewData["IdEstadoDeUnidad"] = new SelectList(_context.EstadosDeUnidad, "IdEstadoDeUnidad", "Descripcion");
            return View();
        }

        // POST: Unidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUnidad,NumeroDePlaca,CapacidadDePasajeros,IdEstadoDeUnidad")] Unidades unidades)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidades);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstadoDeUnidad"] = new SelectList(_context.EstadosDeUnidad, "IdEstadoDeUnidad", "Descripcion", unidades.IdEstadoDeUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = unidades.GetType().Name,
                Objeto = unidades.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(unidades);
        }

        // GET: Unidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new Unidades().GetType().Name,
                    Objeto = new Unidades().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var unidades = await _context.Unidades.FindAsync(id);
            if (unidades == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            ViewData["IdEstadoDeUnidad"] = new SelectList(_context.EstadosDeUnidad, "IdEstadoDeUnidad", "Descripcion", unidades.IdEstadoDeUnidad);
            return View(unidades);
        }

        // POST: Unidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUnidad,NumeroDePlaca,CapacidadDePasajeros,IdEstadoDeUnidad")] Unidades unidades)
        {
            if (id != unidades.IdUnidad)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
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
                    _context.Update(unidades);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadesExists(unidades.IdUnidad))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = unidades.GetType().Name,
                            Objeto = unidades.ToString(),
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
                    Accion = "Crear",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstadoDeUnidad"] = new SelectList(_context.EstadosDeUnidad, "IdEstadoDeUnidad", "Descripcion", unidades.IdEstadoDeUnidad);

            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = unidades.GetType().Name,
                Objeto = unidades.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(unidades);
        }

        // GET: Unidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new Unidades().GetType().Name,
                    Objeto = new Unidades().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var unidades = await _context.Unidades
                .Include(u => u.IdEstadoDeUnidadNavigation)
                .FirstOrDefaultAsync(m => m.IdUnidad == id);
            if (unidades == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = unidades.GetType().Name,
                    Objeto = unidades.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(unidades);
        }

        // POST: Unidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidades = await _context.Unidades.FindAsync(id);
            _context.Unidades.Remove(unidades);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = unidades.GetType().Name,
                Objeto = unidades.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool UnidadesExists(int id)
        {
            return _context.Unidades.Any(e => e.IdUnidad == id);
        }
    }
}
