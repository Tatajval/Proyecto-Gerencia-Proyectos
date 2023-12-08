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
    public class EstadosDeUnidadController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public EstadosDeUnidadController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: EstadosDeUnidad
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadosDeUnidad.ToListAsync());
        }

        // GET: EstadosDeUnidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new EstadosDeUnidad().GetType().Name,
                    Objeto = new EstadosDeUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var estadosDeUnidad = await _context.EstadosDeUnidad
                .FirstOrDefaultAsync(m => m.IdEstadoDeUnidad == id);
            if (estadosDeUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad .ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = estadosDeUnidad.GetType().Name,
                Objeto = estadosDeUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(estadosDeUnidad);
        }

        // GET: EstadosDeUnidad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadosDeUnidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEstadoDeUnidad,Estado,Descripcion")] EstadosDeUnidad estadosDeUnidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadosDeUnidad);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = estadosDeUnidad.GetType().Name,
                Objeto = estadosDeUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(estadosDeUnidad);
        }

        // GET: EstadosDeUnidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new EstadosDeUnidad().GetType().Name,
                    Objeto = new EstadosDeUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var estadosDeUnidad = await _context.EstadosDeUnidad.FindAsync(id);
            if (estadosDeUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            return View(estadosDeUnidad);
        }

        // POST: EstadosDeUnidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEstadoDeUnidad,Estado,Descripcion")] EstadosDeUnidad estadosDeUnidad)
        {
            if (id != estadosDeUnidad.IdEstadoDeUnidad)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad.ToString(),
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
                    _context.Update(estadosDeUnidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadosDeUnidadExists(estadosDeUnidad.IdEstadoDeUnidad))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = estadosDeUnidad.GetType().Name,
                            Objeto = estadosDeUnidad.ToString(),
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
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = estadosDeUnidad.GetType().Name,
                Objeto = estadosDeUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(estadosDeUnidad);
        }

        // GET: EstadosDeUnidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new EstadosDeUnidad().GetType().Name,
                    Objeto = new EstadosDeUnidad().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var estadosDeUnidad = await _context.EstadosDeUnidad
                .FirstOrDefaultAsync(m => m.IdEstadoDeUnidad == id);
            if (estadosDeUnidad == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = estadosDeUnidad.GetType().Name,
                    Objeto = estadosDeUnidad.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(estadosDeUnidad);
        }

        // POST: EstadosDeUnidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadosDeUnidad = await _context.EstadosDeUnidad.FindAsync(id);
            _context.EstadosDeUnidad.Remove(estadosDeUnidad);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = estadosDeUnidad.GetType().Name,
                Objeto = estadosDeUnidad.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool EstadosDeUnidadExists(int id)
        {
            return _context.EstadosDeUnidad.Any(e => e.IdEstadoDeUnidad == id);
        }
    }
}
