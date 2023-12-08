using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;
using BackEnd.Datos;
using BackEnd.Negocio;
using BackEnd.Entidades;
using Microsoft.Data.SqlClient;
using System.Collections;

namespace FrontEnd.Controllers
{
    public class AccesoController : Controller
    {
        private readonly RutasContext _rutas;
        private readonly IActividades actividades;

        public AccesoController()
        {
            _rutas = new RutasContext();
            actividades = new Actividades();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccesoDenegado()
        {
            return View();
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticacion(Usuarios login)
        {
            // /*
            IAcceso _acceso = new Acceso();
            var _usuario = await _rutas.Usuarios.FirstOrDefaultAsync(x => x.Usuario == login.Usuario);
            _usuario.RolesPorUsuario = _acceso.ConfigurarRoles(_usuario, _rutas.RolesPorUsuario.ToList(), _rutas.Roles.ToList());

            int codErr = _acceso.UsuarioValido(_usuario, login.Contrasena);

            if (codErr != 0)
            {
                actividades.Agregar(new Actividad()
                {
                    Accion = "Iniciar",
                    Tipo = "Sesion",
                    Objeto = "Portal administrativo",
                    Usuario = _acceso.MensajeResultadoValidacion(codErr) + login,
                    Completada = false,
                    FechaHora = DateTime.Now
                });

                return RedirectToAction("ErrorAcceso");
            }

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(_acceso.Identidad(_usuario)));

            _usuario.Contrasena = "[redacted]";
            actividades.Agregar(new Actividad()
            {
                Accion = "Iniciar",
                Tipo = "Sesion",
                Objeto = "Portal administrativo",
                Usuario = _usuario.ToString(),
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction("Index", "Home");
            // */
        }

        [HttpGet]
        public async Task<IActionResult> CerrarSesion()
        {
            var _usuario = await _rutas.Usuarios.FirstOrDefaultAsync(x => x.Usuario == HttpContext.User.Identity.Name);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            _usuario.Contrasena = "[redacted]";
            actividades.Agregar(new Actividad()
            {
                Accion = "Finalizar",
                Tipo = "Sesion",
                Objeto = "Portal administrativo",
                Usuario = _usuario.ToString(),
                Completada = true,
                FechaHora = DateTime.Now
            });

            return RedirectToAction("Index", "Acceso");
        }
    }
}
