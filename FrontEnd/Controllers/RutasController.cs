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
    public class RutasController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public RutasController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rutas.ToListAsync());
        }

        // GET: Rutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new Rutas().GetType().Name,
                    Objeto = new Rutas().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rutas = await _context.Rutas
                .FirstOrDefaultAsync(m => m.IdRuta == id);
            if (rutas == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = rutas.GetType().Name,
                Objeto = rutas.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(rutas);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRuta,Ruta,CantidadDeParadas,PrecioPorPersona,EstaActivo")] Rutas rutas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rutas);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = rutas.GetType().Name,
                Objeto = rutas.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(rutas);
        }

        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new Rutas().GetType().Name,
                    Objeto = new Rutas().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rutas = await _context.Rutas.FindAsync(id);
            if (rutas == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            return View(rutas);
        }

        // POST: Rutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRuta,Ruta,CantidadDeParadas,PrecioPorPersona,EstaActivo")] Rutas rutas)
        {
            if (id != rutas.IdRuta)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
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
                    _context.Update(rutas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutasExists(rutas.IdRuta))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = rutas.GetType().Name,
                            Objeto = rutas.ToString(),
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
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = rutas.GetType().Name,
                Objeto = rutas.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(rutas);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new Rutas().GetType().Name,
                    Objeto = new Rutas().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rutas = await _context.Rutas
                .FirstOrDefaultAsync(m => m.IdRuta == id);
            if (rutas == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = rutas.GetType().Name,
                    Objeto = rutas.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(rutas);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rutas = await _context.Rutas.FindAsync(id);
            _context.Rutas.Remove(rutas);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = rutas.GetType().Name,
                Objeto = rutas.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool RutasExists(int id)
        {
            return _context.Rutas.Any(e => e.IdRuta == id);
        }
    }
}
