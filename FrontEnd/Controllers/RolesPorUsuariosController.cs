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
    public class RolesPorUsuariosController : Controller
    {
        private readonly RutasContext _context;
        private readonly IActividades actividades;

        public RolesPorUsuariosController()
        {
            _context = new RutasContext();
            actividades = new Actividades();
        }

        // GET: RolesPorUsuarios
        public async Task<IActionResult> Index()
        {
            var rutasContext = _context.RolesPorUsuario.Include(r => r.IdRolNavigation).Include(r => r.IdUsuarioNavigation);
            return View(await rutasContext.ToListAsync());
        }

        // GET: RolesPorUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = new RolesPorUsuario().GetType().Name,
                    Objeto = new RolesPorUsuario().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rolesPorUsuario = await _context.RolesPorUsuario
                .Include(r => r.IdRolNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolesPorUsuario == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Consultar",
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            actividades.Agregar(new Actividad()
            {
                Accion = "Consultar",
                Tipo = rolesPorUsuario.GetType().Name,
                Objeto = rolesPorUsuario.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return View(rolesPorUsuario);
        }

        // GET: RolesPorUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Rol");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario");
            return View();
        }

        // POST: RolesPorUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdRol")] RolesPorUsuario rolesPorUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolesPorUsuario);
                await _context.SaveChangesAsync();

                actividades.Agregar(new Actividad()
                {
                    Accion = "Asignar",
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Rol", rolesPorUsuario.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario", rolesPorUsuario.IdUsuario);

            actividades.Agregar(new Actividad()
            {
                Accion = "Asignar",
                Tipo = rolesPorUsuario.GetType().Name,
                Objeto = rolesPorUsuario.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(rolesPorUsuario);
        }

        // GET: RolesPorUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = new RolesPorUsuario().GetType().Name,
                    Objeto = new RolesPorUsuario().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rolesPorUsuario = await _context.RolesPorUsuario.FindAsync(id);
            if (rolesPorUsuario == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Rol", rolesPorUsuario.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario", rolesPorUsuario.IdUsuario);
            return View(rolesPorUsuario);
        }

        // POST: RolesPorUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdRol")] RolesPorUsuario rolesPorUsuario)
        {
            if (id != rolesPorUsuario.Id)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Modificar",
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
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
                    _context.Update(rolesPorUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesPorUsuarioExists(rolesPorUsuario.Id))
                    {
                        actividades.Agregar(new Actividad()
                        {
                            Accion = "Modificar",
                            Tipo = rolesPorUsuario.GetType().Name,
                            Objeto = rolesPorUsuario.ToString(),
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
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = true,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Roles, "IdRol", "Rol", rolesPorUsuario.IdRol);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Usuario", rolesPorUsuario.IdUsuario);

            actividades.Agregar(new Actividad()
            {
                Accion = "Modificar",
                Tipo = rolesPorUsuario.GetType().Name,
                Objeto = rolesPorUsuario.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = false,
                FechaHora = DateTime.Now
            });

            return View(rolesPorUsuario);
        }

        // GET: RolesPorUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = new RolesPorUsuario().GetType().Name,
                    Objeto = new RolesPorUsuario().ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            var rolesPorUsuario = await _context.RolesPorUsuario
                .Include(r => r.IdRolNavigation)
                .Include(r => r.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rolesPorUsuario == null)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Eliminar",
                    Tipo = rolesPorUsuario.GetType().Name,
                    Objeto = rolesPorUsuario.ToString(),
                    Usuario = HttpContext.User.Identity.Name,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return NotFound();
            }

            return View(rolesPorUsuario);
        }

        // POST: RolesPorUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolesPorUsuario = await _context.RolesPorUsuario.FindAsync(id);
            _context.RolesPorUsuario.Remove(rolesPorUsuario);
            await _context.SaveChangesAsync();

            actividades.Agregar(new Actividad()
            {
                Accion = "Eliminar",
                Tipo = rolesPorUsuario.GetType().Name,
                Objeto = rolesPorUsuario.ToString(),
                Usuario = HttpContext.User.Identity.Name,
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        private bool RolesPorUsuarioExists(int id)
        {
            return _context.RolesPorUsuario.Any(e => e.Id == id);
        }
    }
}
