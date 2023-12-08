using BackEnd.Datos;
using BackEnd.Entidades;
using BackEnd.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class UsuariosPorRolController : Controller
    {
        private readonly RutasContext _context;

        public UsuariosPorRolController()
        {
            _context = new RutasContext();
        }

        // GET: UsuariosPorEstadoController
        public IActionResult Index()
        {
            return View(_context.VwUsuariosPorRol.AsAsyncEnumerable());
        }
    }
}
