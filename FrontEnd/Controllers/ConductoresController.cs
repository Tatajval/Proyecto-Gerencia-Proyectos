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
    public class ConductoresController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public ConductoresController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: Conductores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conductores.ToListAsync());
        }

        // GET: Conductores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new Conductores().GetType().Name,
                    Objeto = new Conductores().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductores = await _context.Conductores
                .FirstOrDefaultAsync(m => m.IdConductor == id);
            if (conductores == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = conductores.GetType().Name,
                Objeto = conductores.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(conductores);
        }

        // GET: Conductores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conductores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConductor,NombreCompleto,EstaActivo,EstaDisponible")] Conductores conductores)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conductores);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Crear",
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Crear",
                Tipo = conductores.GetType().Name,
                Objeto = conductores.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(conductores);
        }

        // GET: Conductores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new Conductores().GetType().Name,
                    Objeto = new Conductores().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductores = await _context.Conductores.FindAsync(id);
            if (conductores == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            return View(conductores);
        }

        // POST: Conductores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConductor,NombreCompleto,EstaActivo,EstaDisponible")] Conductores conductores)
        {
            if (id != conductores.IdConductor)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
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
                    _context.Update(conductores);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConductoresExists(conductores.IdConductor))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = conductores.GetType().Name,
                            Objeto = conductores.ToString(),
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
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = conductores.GetType().Name,
                Objeto = conductores.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(conductores);
        }

        // GET: Conductores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new Conductores().GetType().Name,
                    Objeto = new Conductores().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var conductores = await _context.Conductores
                .FirstOrDefaultAsync(m => m.IdConductor == id);
            if (conductores == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = conductores.GetType().Name,
                    Objeto = conductores.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(conductores);
        }

        // POST: Conductores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conductores = await _context.Conductores.FindAsync(id);
            _context.Conductores.Remove(conductores);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = conductores.GetType().Name,
                Objeto = conductores.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool ConductoresExists(int id)
        {
            return _context.Conductores.Any(e => e.IdConductor == id);
        }
    }
}
